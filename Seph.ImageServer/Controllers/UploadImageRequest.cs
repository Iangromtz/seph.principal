namespace Seph.ImageServer.Controllers
{
    /// <summary>
    /// Swashbuckle no soporta un IFormFile como parámetro suelto de acción
    /// junto con otro campo [FromForm]; hay que agruparlos en un modelo.
    /// </summary>
    public sealed class UploadImageRequest
    {
        public string Tipo { get; set; } = string.Empty;
        public IFormFile Archivo { get; set; } = null!;
    }
}
