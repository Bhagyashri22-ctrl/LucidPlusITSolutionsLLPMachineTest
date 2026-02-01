using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest2026.CustomeModel
{
    public class EmpModel
    {

        public long Id { get; set; }
        [Required(ErrorMessage ="Name Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "MobileNo Required!")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="10 digit required")]
        [RegularExpression(@"^\d+$",ErrorMessage ="Only Digit Required!")]
        public string MobNo { get; set; }
        [Required(ErrorMessage = "Email  Required!")]
        [EmailAddress(ErrorMessage ="Invalid Email!")]
        public string EmailId { get; set; }
       

        public bool Active { get; set; }
        public bool Delete { get; set; }

        public DateTime? CreatedOn { get; set; }
     

        public long? CountryId { get; set; }
        public string? CountryName { get; set; }
       
        public List<IFormFile>?UploadFileList { get; set; }
        public List<string>? ShowFileList { get; set; }
        public long? CreatedBy { get; set; }

    }
}
