using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimManagementSystem.Models;
using SimManagementSystem.DAL;
using SimManagementSystem.CommonUtility;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class MSISDController : Controller
    {
        MsisdDAL db = new MsisdDAL();
        UserDAL userDal = new UserDAL();
        WebHelper web = new WebHelper();
        UserViewModel user = new UserViewModel();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetMSISD()
        {
            var val = web.GetUserIdentityFromSession();
            if(val.Name == "Admin")
            {
                var list = db.GetMSISD(); 
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.GetMSISD().Where(x => x.Added_By == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Create(MSISDViewModel mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.Added_By = resModel.EmployeeName;
            db.SaveMSISD(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(MSISDViewModel mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.Modify_By = resModel.EmployeeName;
            db.EditMSISD(mSISDViewModel);
                return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(MSISDViewModel mSISDViewModel) 
        {
            db.DeleteMSISD(mSISDViewModel.ID); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
