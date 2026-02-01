using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest2026.DomainModel
{
    [Table("UserTBL")]
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
