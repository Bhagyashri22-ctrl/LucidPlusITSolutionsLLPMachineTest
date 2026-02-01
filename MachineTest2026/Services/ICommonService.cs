namespace MachineTest2026.Services
{
    public interface ICommonService
    {

        string UploadFile(IFormFile file, string folderName);
        string GetBaseUrl();

    }
}
