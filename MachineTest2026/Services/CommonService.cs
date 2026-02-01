using System.Text.RegularExpressions;

namespace MachineTest2026.Services
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CommonService(IHttpContextAccessor contextAccessor) 
        {
            _contextAccessor = contextAccessor;
        }
        


        public string GetBaseUrl()
        {
             var context= _contextAccessor.HttpContext;
            if(context== null )
            {
                return string.Empty;
            }
            else
            {
                var request= context.Request;
                return $"{request.Scheme}://{request.Host.Value}/";
            }
        }

        public string UploadFile(IFormFile file, string folderName)
        {
            if(file!= null) { 
            var originalName=Path.GetFileName(file.FileName);
                var cleanName = Regex.Replace(originalName, @"[^a-zA-Z0-9.]", "");
                var fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{cleanName}";

                var uploadFolde = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                var fullPath= Path.Combine(uploadFolde, fileName);

                using(var stream=new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);    
                }
                return $"{folderName}/{fileName}";                                                                                                                                                                              

            }
            else
            {
                return null;
            }

        }
    }
}
