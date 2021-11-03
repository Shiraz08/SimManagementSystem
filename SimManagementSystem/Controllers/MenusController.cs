using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SimManagementSystem.DAL.MsisdDAL;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class MenusController : Controller
    {
        MenuDAL msisdDAL = new MenuDAL();
        UserDAL userDal = new UserDAL();
        [HttpGet]
        public JsonResult Checks(long UserID)
        {
            List<Menu_ID> local = msisdDAL.GetChecks(UserID);      
            return Json(msisdDAL.GetChecks(UserID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Names(long UserID,string[] values)
        {
            if (values[0] == "[]")
            {
                msisdDAL.DeleteAssigns(UserID);
            }
            else
            {
                var _val = "";
                foreach (var item in Convert.ToString(values[0]).Split(',', '"', ' ', '[', ']'))
                {
                    if (item != "")
                    {
                       _val += item + ",";
                    }
                }
                var mu = msisdDAL.GetMenusNames().Select(x => x.ID).ToList();
                foreach(var del in mu)
                {
                    msisdDAL.DeleteById(UserID,Convert.ToInt32(del));
                }
                var lists = _val.TrimEnd(',');
                foreach (var i in lists.Split(',')) 
                {
                    msisdDAL.SaveAssigns(UserID, Convert.ToInt32(i));
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public List<EmployeeData> GetEmployeeInfo()
        {
            List<EmployeeData> res = new List<EmployeeData>();
            res = userDal.GetEmployeeInfo();
            return res; 
        }
        public ActionResult AssignMenu()
        {
            ViewBag.EmpList = GetEmployeeInfo().Select(x => new SelectListItem { Text = x.EmployeeName.ToString() + " " + "-" + " " + x.EMPID.ToString(), Value = x.EMPID.ToString() }).Distinct().ToList();
            ViewBag.Menus = msisdDAL.GetMenusNames();
            return View();
        }
    }
}