using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class AssignMSISDController : Controller
    {
        UserDAL userDal = new UserDAL();
        MsisdDAL dbs = new MsisdDAL();
        WebHelper web = new WebHelper();
        UserViewModel user = new UserViewModel();
        AssignMSISDDAL db = new AssignMSISDDAL();
        public ActionResult Index()
        {
            var val =  dbs.GetMSISD();
            ViewBag.msisd = dbs.GetMSISD().Select(x => new SelectListItem { Text = x.Sim_MSISD_No.ToString(), Value = x.Sim_MSISD_No.ToString() }).Distinct().ToList();
            return View();
        }
        public JsonResult GetAssignMSISD()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = db.GetAssignMSISD();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.GetAssignMSISD().Where(x => x.Added_By == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Create(AssignMSISDViewModel mSISDViewModel) 
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.Added_By = resModel.EmployeeName;
            db.SaveMSISD(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(AssignMSISDViewModel mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.Modify_By = resModel.EmployeeName;
            db.EditMSISD(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(AssignMSISDViewModel mSISDViewModel) 
        {
            db.DeleteMSISD(mSISDViewModel.ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}