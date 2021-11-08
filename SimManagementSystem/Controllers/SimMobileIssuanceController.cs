using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class SimMobileIssuanceController : Controller
    {
        SimMobileIssuanceDAL db = new SimMobileIssuanceDAL();
        AssignedSimNewPartnerDAL dal = new AssignedSimNewPartnerDAL();
        WebHelper web = new WebHelper();
        public JsonResult Delete(SimMobileIssuance mSISDViewModel)
        {
            db.Delete(mSISDViewModel.ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddSimMobileIssuance()
        {
            ViewBag.VoicePackageName = dal.GetVoicePackageName().Select(x => new SelectListItem { Text =  x.Package_Description + " " + ">" + " " + x.Package_Tax_Description, Value = x.VPN_ID.ToString() }).Distinct().ToList();
            ViewBag.DataPackageName = dal.GetDataPackageName().Select(x => new SelectListItem { Text = x.DP_Description.ToString() , Value = x.DP_ID.ToString() }).Distinct().ToList();
            ViewBag.BType = db.GetBType().Select(x => new SelectListItem { Text = x.Btype_Name.ToString(), Value = x.ID.ToString() }).Distinct().ToList();
            ViewBag.MobileNumber = db.GetMobileNumber().Select(x => new SelectListItem { Text = x.MobileNumber.ToString(), Value = x.MobileNumber.ToString() }).Distinct().ToList();
            ViewBag.Department = db.GetDepartment().Select(x => new SelectListItem { Text = x.Description.ToString(), Value = x.DepttID.ToString() }).Distinct().ToList();
            ViewBag.Campus = db.GetCampus().Select(x => new SelectListItem { Text = x.campusname.ToString(), Value = x.id.ToString() }).Distinct().ToList();
            ViewBag.EmployeeInfo = db.GetEmployeeInfo().Select(x => new SelectListItem { Text = x.EmployeeName.ToString() + " " + "-" + " " + x.EMPID.ToString(), Value = x.EMPID.ToString() }).Distinct().ToList();
            return View();
        }
        public ActionResult ReturnSim()
        {
            ViewBag.MobileNumber = db.GetAssignMobileNumber().Select(x => new SelectListItem { Text = x.MobileNumber.ToString(), Value = x.MobileNumber.ToString() }).Distinct().ToList();
            return View();
        } 
        [HttpPost]
        public JsonResult EditData(SimMobileIssuanceViewModel simMobileIssuance)
        {
            db.EditSimMobileIssuance(simMobileIssuance);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Returnsavesim(SimMobileIssuanceViewModel simMobileIssuance)
        {
            db.UpdateReturnNUmberStatus(simMobileIssuance); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(SimMobileIssuance simMobileIssuance)
        {

            var vals = Regex.Replace(simMobileIssuance.EmployeeName, @"\s+", "");
            simMobileIssuance.EmployeeName = vals.ToString();
            db.SaveSimMobileIssuance(simMobileIssuance);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = db.GetSimMobileIssuanceData();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = db.GetSimMobileIssuanceData().Where(x => x.AddedBy == val.Name).ToList(); 
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getEmployeeCNIC(string value)
        {
            var data = db.GetEmployeeCNIC(value);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}