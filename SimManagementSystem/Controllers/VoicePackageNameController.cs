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
    public class VoicePackageNameController : Controller
    {
        VoicePackageNameDAL db = new VoicePackageNameDAL();
        UserDAL userDal = new UserDAL();
        UserViewModel user = new UserViewModel();
        WebHelper web = new WebHelper();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetVoicePackageName()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = db.GetVoicePackageName();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.GetVoicePackageName().Where(x => x.AddedBy == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Create(VoicePackageNameModel mVoicePackageName)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mVoicePackageName.AddedBy = resModel.EmployeeName;
            db.SaveVoicePackageName(mVoicePackageName);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(VoicePackageNameModel mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession(); 
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.ModifiedBy = resModel.EmployeeName; 
            db.EditVoicePackageName(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(VoicePackageNameModel mSISDViewModel)
        {
            db.DeleteVoicePackageName(mSISDViewModel.VPN_ID);  
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}