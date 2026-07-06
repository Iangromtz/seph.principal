namespace Seph.ImageServer
{
    public sealed class UploadsOptions
    {
        public const string SectionName = "Uploads";

        public string RootPath { get; set; } = string.Empty;
        public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;
    }
}
