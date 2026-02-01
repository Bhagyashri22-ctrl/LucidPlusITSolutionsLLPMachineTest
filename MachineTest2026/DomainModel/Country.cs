using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest2026.DomainModel
{
    [Table("CountryTBL")]
    public class Country
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Emp> Emps { get; set; }

    }
}
