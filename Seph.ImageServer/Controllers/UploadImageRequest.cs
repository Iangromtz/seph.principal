namespace Seph.ImageServer.Controllers
{
    public sealed class UploadImageRequest
    {
        public string Tipo { get; set; } = string.Empty;
        public IFormFile Archivo { get; set; } = null!;
    }
}
