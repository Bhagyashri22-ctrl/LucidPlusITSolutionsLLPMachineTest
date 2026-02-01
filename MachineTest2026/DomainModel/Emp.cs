using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest2026.DomainModel
{
    [Table("EmpTBL")]
    public class Emp
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public string MobNo { get; set; }
        public string EmailId { get; set; }

       
        public bool Active  { get; set; }
        public bool Delete { get; set; }

        public DateTime CreatedOn { get; set; }
       
        [ForeignKey("CountryTBL")]
        public long? CountryId { get; set; }
       
        public long? CreatedBy { get; set; }


       
        public virtual Country Country { get; set; }
        
        public List<EmpProfile>  EmpProfiles{ get; set; }


    }
}
