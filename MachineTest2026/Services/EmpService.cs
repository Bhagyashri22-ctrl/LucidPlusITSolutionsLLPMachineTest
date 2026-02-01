using MachineTest2026.CustomeModel;
using MachineTest2026.DomainModel;

namespace MachineTest2026.Services
{
    public class EmpService : IEmpService
    {
      private readonly CompanyContext _companyContext;
        private readonly ICommonService _commonService;


        public EmpService(CompanyContext companyContext, ICommonService commonServic) 
        {
            _companyContext = companyContext;
            _commonService = commonServic;

        }
        public void DeleteEmp(Emp model)
        {
            var res=this._companyContext.Emps.Find(model.Id);
            this._companyContext.Emps.Remove(res);
            this._companyContext.SaveChanges();

        }

        public void DeleteEmpProfile(EmpProfile model)
        {
            var res = this._companyContext.EmpProfiles.Find(model.Id);
            this._companyContext.EmpProfiles.Remove(res);
            this._companyContext.SaveChanges();
        }

       

        public List<Country> GetAllCountry()
        {
             var res=this._companyContext.Countries.ToList();
            return res;
        }

        

        public IList<EmpModel> GetAllEmpList(long LoginId)
        {
             var baseurl=this._commonService.GetBaseUrl();
            var res = from em in this._companyContext.Emps
                      where  em.Delete == false&& em.CreatedBy== LoginId
                      orderby em.Id descending
                      select new EmpModel
                      {
                          Id= em.Id,
                          Name= em.Name,    
                          MobNo= em.MobNo,
                          EmailId= em.EmailId,  
                          Active= em.Active,
                          CountryId= em.CountryId,
                          CountryName=em.Country.Name,
                          CreatedOn= em.CreatedOn,  
                           ShowFileList=em.EmpProfiles.Where(p=>p.EmpId==em.Id)
                           .Select(p=> baseurl+p.EmpProfileFiles).ToList()
                          


                      };
            return res.ToList();



        }

        public IList<EmpProfile> GetAllEmpProfilesByEmpId(long Id)
        {
             var res=this._companyContext.EmpProfiles.Where(p=>p.EmpId==Id).ToList();
            return res;
        }

       

        public Emp GetEmpById(long Id)
        {
            var res = this._companyContext.Emps.Where(p => p.Id == Id).FirstOrDefault();
            return res;
        }

        public Emp GetEmpByMobileNo(string MobileNo, long Id)
        {
            var res = this._companyContext.Emps.Where(p => p.MobNo == MobileNo&&p.Id!=Id).FirstOrDefault();
            return res;
        }

        public EmpModel GetEmpDetailsById(long Id)
        {
            var baseurl = this._commonService.GetBaseUrl();

            var res = from em in this._companyContext.Emps
                      where  em.Delete == false&& em.Id==Id
                      select new EmpModel
                      {
                          Id = em.Id,
                          Name = em.Name,
                          MobNo = em.MobNo,
                          EmailId = em.EmailId,
                          Active = em.Active,
                          CountryId = em.CountryId,
                          CountryName = em.Country.Name,
                          CreatedOn = em.CreatedOn,

                          ShowFileList = em.EmpProfiles.Where(p => p.EmpId == em.Id)
                           .Select(p => baseurl + p.EmpProfileFiles).ToList()




                      };
            return res.FirstOrDefault();
                
        }

        public EmpProfile GetEmpProfileById(long Id)
        {
            var res = this._companyContext.EmpProfiles.Where(p => p.Id == Id).FirstOrDefault();
            return res;
        }

        public User GetUserById(long Id)
        {
            var res = this._companyContext.Users.Where(p =>  p.Id != Id).FirstOrDefault();
            return res;
        }

        public User GetUserByUserName(string UserName, long Id)
        {
            var res = this._companyContext.Users.Where(p => p.UserName == UserName && p.Id != Id).FirstOrDefault();
            return res;
        }

        public void InsertEmp(Emp model)
        {

           this._companyContext.Emps.Add(model);
            this._companyContext.SaveChanges();
        }

        public void InsertEmpProfile(EmpProfile model)
        {
            this._companyContext.EmpProfiles.Add(model);
            this._companyContext.SaveChanges();
        }

        public void InserUser(User model)
        {
            this._companyContext.Users.Add(model);
            this._companyContext.SaveChanges();
        }

        public LoginResultModel SignIn(User model)
        {

            var res = from ur in this._companyContext.Users
                      where ur.UserName == model.UserName && ur.Password == model.Password
                      select new LoginResultModel
                      {
                          LoggedInId = ur.Id,
                          LoggedInName = ur.UserName,
                          IsSuccess = true,
                      };
            return res.FirstOrDefault();
        }

        public void UpdateEmp(Emp model)
        {
            this._companyContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this._companyContext.SaveChanges();
        }
       
    }
}
