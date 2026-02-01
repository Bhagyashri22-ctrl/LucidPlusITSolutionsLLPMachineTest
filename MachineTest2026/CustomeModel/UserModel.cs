using System.ComponentModel.DataAnnotations;

namespace MachineTest2026.CustomeModel
{
    public class UserModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="User Name Required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required!")]

        public string Password { get; set; }
    }
}
