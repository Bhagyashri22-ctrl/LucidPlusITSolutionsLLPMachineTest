using MachineTest2026.CustomeModel;
using MachineTest2026.DomainModel;

namespace MachineTest2026.Services
{
    public interface IEmpService
    {
        IList<EmpModel> GetAllEmpList(long LoginId);
        EmpModel GetEmpDetailsById(long Id);
        Emp GetEmpById(long Id);

        Emp GetEmpByMobileNo(string MobileNo, long Id);
        void InsertEmp(Emp model);
        void UpdateEmp(Emp model);
        void DeleteEmp(Emp model);

        IList<EmpProfile> GetAllEmpProfilesByEmpId(long Id);

        EmpProfile GetEmpProfileById(long Id);

        void DeleteEmpProfile(EmpProfile model);
        void InsertEmpProfile(EmpProfile model);

        List<Country> GetAllCountry();


        void InserUser(User model);
        User GetUserById(long Id);
        User GetUserByUserName(string UserName, long Id);

        LoginResultModel SignIn(User model);
    }
}
