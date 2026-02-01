using MachineTest2026.CustomeModel;
using MachineTest2026.DomainModel;
using MachineTest2026.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachineTest2026.Controllers
{
    public class EmpMVCController : Controller
    {
        private readonly IEmpService _empService;
        private readonly ICommonService _commonService;

        public EmpMVCController(IEmpService empService, ICommonService commonService)
        {
            _empService = empService;
            _commonService = commonService;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var existUser = this._empService.GetUserByUserName(model.UserName, 0);
                if (existUser == null)
                {
                    User ur = new User();
                    ur.UserName = model.UserName;
                    ur.Password = model.Password;
                    this._empService.InserUser(ur);
                    TempData["Message"] = "SignUp Successfully!";
                    return RedirectToAction("SignIn");
                }
                TempData["Message"] = "UserName Already Exist!";
                return View();

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(User model)
        {
            if (ModelState.IsValid)
            {
                var existUser = this._empService.SignIn(model);
                
                    if (existUser!=null)
                    {
                        HttpContext.Session.SetString("UserId", existUser.LoggedInId.ToString());
                        HttpContext.Session.SetString("UserName", existUser.LoggedInName);
                        TempData["Message"] = "SignIn Successfully!";
                        return RedirectToAction("DashBoard");
                    }
                    TempData["Message"] = "Invalid UserName Or Password!";
                    return RedirectToAction("SignIn");
                
                

            }
            return View();
        }

        [HttpGet]
        public IActionResult DashBoard()
        {
            long LoginId = GetSessionId();
            var res = this._empService.GetAllEmpList(LoginId);
            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name");
            

            if (res.Count > 0)
            {
                return View(res);

            }
            return View(res);

        }

        [HttpGet]
        public IActionResult InserEmp()
        {
            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name");
            
            return View();
        }

        [HttpPost]
        public IActionResult InserEmp(EmpModel model)
        {
            model.CreatedBy=GetSessionId();
          
            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name");
          
            if (ModelState.IsValid)
            {


                Emp em = new Emp();
                em.Name = model.Name;
                em.MobNo = model.MobNo;
                em.EmailId = model.EmailId;
               
                em.CountryId = model.CountryId;
                em.Active = true;
                em.CreatedOn = DateTime.Now;
                em.CreatedBy = model.CreatedBy;
                this._empService.InsertEmp(em);

                if (model.UploadFileList!=null)
                {

                    foreach (var temp in model.UploadFileList)
                    {
                        if (temp.ContentType != "image/png" && temp.ContentType != "image/jpg" && temp.ContentType != "image/jpeg")
                        {
                            TempData["Message"] = "Iamges Must Be Png ,jpg ,jpeg";

                            return View(model);
                        }
                        if (temp.Length > 2 * 1024 * 1024)
                        {
                            TempData["Message"] = "Image must be less than 2 mb!";

                            return View(model);

                        }
                    }

                    foreach (var temp in model.UploadFileList)
                    {
                        var file = this._commonService.UploadFile(temp, UploadFiles.EMPPROFILE);
                        EmpProfile pr = new EmpProfile();
                        pr.EmpId = em.Id;
                        pr.EmpProfileFiles = file;
                        this._empService.InsertEmpProfile(pr);
                    }
                }

                return RedirectToAction("DashBoard");
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult EditEmp(long Id)
        {
            var res = this._empService.GetEmpDetailsById(Id);
            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name", res.CountryId);

            return View(res);
        }

        [HttpPost]
        public IActionResult EditEmp(EmpModel model)
        {
            var res = this._empService.GetEmpDetailsById(model.Id);

            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name", res.CountryId);

            model.ShowFileList = res.ShowFileList;

            if (ModelState.IsValid)
            {
                var em = this._empService.GetEmpById(model.Id);
                em.Name = model.Name;
                em.MobNo = model.MobNo;
                em.EmailId = model.EmailId;
                
                em.CountryId = model.CountryId;
                em.Active = model.Active;
                this._empService.UpdateEmp(em);

                if (model.UploadFileList!=null)
                {
                    foreach (var temp in model.UploadFileList)
                    {
                        if (temp.ContentType != "image/png" && temp.ContentType != "image/jpg" && temp.ContentType != "image/jpeg")
                        {
                            TempData["Message"] = "Iamges Must Be Png ,jpg ,jpeg";
                            return View(model);
                        }
                        if (temp.Length > 2 * 1024 * 1024)
                        {
                            TempData["Message"] = "Image must be less than 2 mb!";
                            return View(model);

                        }
                    }
                    var profileList = this._empService.GetAllEmpProfilesByEmpId(model.Id);
                    foreach (var temp in profileList)
                    {
                        var existProfile = this._empService.GetEmpProfileById(temp.Id);
                        this._empService.DeleteEmpProfile(existProfile);

                    }
                    foreach (var temp in model.UploadFileList)
                    {
                        var file = this._commonService.UploadFile(temp, UploadFiles.EMPPROFILE);
                        EmpProfile pr = new EmpProfile();
                        pr.EmpId = em.Id;
                        pr.EmpProfileFiles = file;
                        this._empService.InsertEmpProfile(pr);
                    }
                }


                return RedirectToAction("DashBoard");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteEmp(long Id)
        {
            var em = this._empService.GetEmpById(Id);
            this._empService.DeleteEmp(em);
            return RedirectToAction("DashBoard");

        }


        [HttpGet]
        public IActionResult DetailsEmp(long Id)
        {
            var em = this._empService.GetEmpDetailsById(Id);
            ViewBag.CountryId = new SelectList(this._empService.GetAllCountry(), "Id", "Name", em.CountryId);

            return View(em);

        }


        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");


        }

        public Int64 GetSessionId()
        {
            Int64 res = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            return res;

        }

       
    }
}
