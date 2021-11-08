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
    public class MobileNumberSeriesController : Controller
    {
        MobileNumberSeriesDAL simDAL = new MobileNumberSeriesDAL();
        WebHelper web = new WebHelper();
        UserViewModel user = new UserViewModel();
        UserDAL userDal = new UserDAL();
        [HttpPost]
        public JsonResult Delete(SimInfo mSISDViewModel)
        {
            simDAL.Delete(mSISDViewModel.ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    
        public JsonResult GetNumbers()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = simDAL.GetNumbers();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = simDAL.GetNumbers().Where(x => x.CreatedBy == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetSim(SimInfo local)
        {
            simDAL.SaveAssigns(local);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSim()
        {
            return View();
        }
        public JsonResult Updatemsisdno(SimInfo local)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            local.ModifiedBy = user.Name; 
            local.ModifiedDate = DateTime.Now;
            simDAL.UpdateMSISD(local); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}