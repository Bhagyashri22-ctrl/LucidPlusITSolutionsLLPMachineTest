using Microsoft.EntityFrameworkCore;

namespace MachineTest2026.DomainModel
{
    public class CompanyContext:DbContext
    {

        public CompanyContext(DbContextOptions<CompanyContext>opt):base(opt) { }


        public DbSet<Country>Countries { get; set; }
       
        public DbSet<Emp> Emps { get; set; }

        public DbSet<EmpProfile> EmpProfiles { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
