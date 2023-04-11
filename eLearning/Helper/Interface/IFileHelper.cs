namespace eLearning.Helper.Interface
{
    public interface IFileHelper
    {
        Task<string> Save(IFormFile file, string folderName);
    }
}