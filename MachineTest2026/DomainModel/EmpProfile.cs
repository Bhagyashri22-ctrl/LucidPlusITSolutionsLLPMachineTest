using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest2026.DomainModel
{
    [Table("EmpProfile")]
    public class EmpProfile
    {
        public long Id { get; set; }

        public string EmpProfileFiles { get; set; }
        [ForeignKey("EmpTBL")]
        public long EmpId { get; set; }

        public virtual Emp Emp { get; set; }
    }
}
