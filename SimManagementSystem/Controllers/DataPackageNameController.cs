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
    public class DataPackageNameController : Controller
    {
        DataPackageNameDAL db = new DataPackageNameDAL();
        UserDAL userDal = new UserDAL();
        UserViewModel user = new UserViewModel();
        WebHelper web = new WebHelper();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetDataPackageName()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = db.GetDataPackageName();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.GetDataPackageName().Where(x => x.AddedBy == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Create(DataPackageNameModel datapackage)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            datapackage.AddedBy = resModel.EmployeeName;
            db.SaveDataPackageName(datapackage); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Edit(DataPackageNameModel datapackage) 
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            datapackage.ModifiedBy = resModel.EmployeeName;
            db.EditDataPackageName(datapackage); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(DataPackageNameModel datapackage) 
        {
            db.DeleteDataPackageName(datapackage.DP_ID); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}