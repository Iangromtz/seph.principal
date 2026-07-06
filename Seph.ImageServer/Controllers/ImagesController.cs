using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Seph.ImageServer.Controllers
{
    /// <summary>
    /// Sube y sirve archivos (INE y fotografía de trabajador) de los empleados.
    /// Protegido con el mismo JWT que emite Seph.Principal: cualquier usuario
    /// autenticado en el sistema principal puede llamar a este servicio.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed partial class ImagesController(
        IOptions<UploadsOptions> uploadsOptions,
        IWebHostEnvironment environment) : ControllerBase
    {
        private static readonly Dictionary<string, string[]> AllowedExtensionsByTipo = new()
        {
            ["ine"] = [".jpg", ".jpeg", ".png", ".pdf"],
            ["foto"] = [".jpg", ".jpeg", ".png"]
        };

        private static readonly Dictionary<string, string> ContentTypeByExtension = new()
        {
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png",
            [".pdf"] = "application/pdf"
        };

        [GeneratedRegex("^[a-f0-9]{32}\\.[a-z]+$")]
        private static partial Regex FileNamePattern();

        /// <summary>
        /// Sube un archivo (INE o fotografía). Devuelve la ruta relativa a
        /// guardar en el registro del empleado (Empleado.StrRutaIne / StrRutaFotografia).
        /// POST /api/v1/images/upload
        /// </summary>
        [HttpPost("upload")]
        [RequestSizeLimit(10_485_760)]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequest request, CancellationToken cancellationToken)
        {
            var tipo = request.Tipo;
            var archivo = request.Archivo;

            if (!AllowedExtensionsByTipo.TryGetValue(tipo, out var allowedExtensions))
            {
                return BadRequest(new { message = "El tipo debe ser 'ine' o 'foto'." });
            }

            if (archivo is null || archivo.Length == 0)
            {
                return BadRequest(new { message = "Debe adjuntar un archivo." });
            }

            if (archivo.Length > uploadsOptions.Value.MaxFileSizeBytes)
            {
                return BadRequest(new { message = $"El archivo no debe superar {uploadsOptions.Value.MaxFileSizeBytes / 1024 / 1024} MB." });
            }

            var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new { message = $"Formato no permitido para '{tipo}'. Formatos válidos: {string.Join(", ", allowedExtensions)}" });
            }

            var rootPath = ResolveRootPath();
            var tipoFolder = Path.Combine(rootPath, tipo);
            Directory.CreateDirectory(tipoFolder);

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(tipoFolder, fileName);

            await using (var stream = System.IO.File.Create(fullPath))
            {
                await archivo.CopyToAsync(stream, cancellationToken);
            }

            return Ok(new { tipo, fileName, rutaRelativa = $"{tipo}/{fileName}" });
        }

        /// <summary>
        /// Descarga un archivo previamente subido. Valida que el nombre de
        /// archivo tenga el formato esperado (evita path traversal).
        /// GET /api/v1/images/{tipo}/{fileName}
        /// </summary>
        [HttpGet("{tipo}/{fileName}")]
        public IActionResult Get(string tipo, string fileName)
        {
            if (!AllowedExtensionsByTipo.ContainsKey(tipo))
            {
                return NotFound();
            }

            if (!FileNamePattern().IsMatch(fileName))
            {
                return NotFound();
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            if (!ContentTypeByExtension.TryGetValue(extension, out var contentType))
            {
                return NotFound();
            }

            var rootPath = ResolveRootPath();
            var fullPath = Path.Combine(rootPath, tipo, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound();
            }

            var stream = System.IO.File.OpenRead(fullPath);
            return File(stream, contentType);
        }

        private string ResolveRootPath()
        {
            var configuredPath = uploadsOptions.Value.RootPath;
            return Path.IsPathRooted(configuredPath)
                ? configuredPath
                : Path.Combine(environment.ContentRootPath, configuredPath);
        }
    }
}
