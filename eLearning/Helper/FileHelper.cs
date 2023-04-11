namespace eLearning.Helper.Interface
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileHelper(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> Save(IFormFile file, string folderName)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filename = Guid.NewGuid() + "." + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(path, filename);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
            return filename;
        }
    }
}