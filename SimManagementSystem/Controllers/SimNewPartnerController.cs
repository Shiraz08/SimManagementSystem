using SimManagementSystem.CommonUtility;
using System;
using System.Collections.Generic;
using SimManagementSystem.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimManagementSystem.DAL;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class SimNewPartnerController : Controller
    {
        UserDAL userDal = new UserDAL();
        AssignedSimNewPartnerDAL dal = new AssignedSimNewPartnerDAL();
        MsisdDAL dbs = new MsisdDAL();
        SimMobileIssuanceDAL m = new SimMobileIssuanceDAL();
        UserViewModel user = new UserViewModel();
        AssignMSISDDAL db = new AssignMSISDDAL();
        WebHelper web = new WebHelper();
        SimMobileIssuanceDAL desigantion = new SimMobileIssuanceDAL();
        public ActionResult Index()
        {
            ViewBag.VoicePackageName = dal.GetVoicePackageName().Select(x => new SelectListItem { Text = x.All_Network_Mints.ToString() + " " + ">" + " " + x.All_Network_SMS + " " + ">" + " " + x.All_Net_Data + " " + ">" + " " + x.Package_Description, Value = x.VPN_ID.ToString() }).Distinct().ToList();
            ViewBag.DataPackageName = dal.GetDataPackageName().Select(x => new SelectListItem { Text = x.DP_Description.ToString() + " " + ">" + " " + x.DP_GB_Type + " " + ">" + " " +  x.DP_Price, Value = x.DP_ID.ToString() }).Distinct().ToList();
            ViewBag.Designation = desigantion.GetDesignation().Select(x => new SelectListItem { Text = x.Designationname.ToString(), Value = x.Designationid.ToString() }).Distinct().ToList();
            ViewBag.MobileNumber = m.GetMobileNumber().Select(x => new SelectListItem { Text = x.MobileNumber.ToString(), Value = x.MobileNumber.ToString() }).Distinct().ToList();
            return View(); 
        }

        public JsonResult GetCreditLimit(string EmployeeDesiginationID , string EmployeeShift)
        {
            if(EmployeeShift == "")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var creditlimit = "";
                if (EmployeeDesiginationID == "87" || EmployeeDesiginationID == "50")
                {
                    creditlimit = "4000";
                }
                else if (EmployeeDesiginationID == "22" || EmployeeDesiginationID == "27")
                {
                    creditlimit = "4000";
                }
                else if (EmployeeDesiginationID == "15" || EmployeeDesiginationID == "34")
                {
                    creditlimit =  EmployeeShift == "Both" ? "4000" : "2000";
                }
                else if (EmployeeDesiginationID == "132" || EmployeeDesiginationID == "16" || EmployeeDesiginationID == "5" || EmployeeDesiginationID == "91")
                {
                    creditlimit =  EmployeeShift == "Both" ? "3000" : "1500";
                }
                else if (EmployeeDesiginationID == "18" || EmployeeDesiginationID == "6" || EmployeeDesiginationID == "17" )
                {
                    creditlimit = EmployeeShift == "Both" ? "2000" : "1000";
                }
                else if (EmployeeDesiginationID == "7")
                {
                    creditlimit = EmployeeShift == "Both" ? "1000" : "500";
                }
                else if (EmployeeDesiginationID == "70")
                {
                    creditlimit =  EmployeeShift == "Both" ? "1000" : "500"; 
                }
                return Json(creditlimit , JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetData()
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = dal.GetSimNewPartner();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = dal.GetSimNewPartner().Where(x => x.Createby == val.Name).ToList(); 
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult CreateSimNewPartner(AssignedSimPartner model)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            model.Createby = resModel.EmployeeName;
            dal.SaveDataAssignedSimPartner(model);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(AssignedSimPartner mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            mSISDViewModel.Modifyby = resModel.EmployeeName;
            dal.EditDataAssignedSimPartner(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(AssignedSimPartner mSISDViewModel) 
        {
            dal.DeleteAssignedSimPartner(mSISDViewModel.ID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApprovedAssignSim(AssignedSimPartner mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession();
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            if (resModel.EmpID == 0)
            {
                mSISDViewModel.Approvedby = "Admin";
            }
            else
            {
                mSISDViewModel.Approvedby = resModel.EmployeeName;
            }
            dal.ApprovedAssignedSimPartner(mSISDViewModel);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnApprovedAssignSim(AssignedSimPartner mSISDViewModel)
        {
            user = new WebHelper().GetUserIdentityFromSession(); 
            EmployeeDetailVM resModel = userDal.GetEmployeeImage(user.UserId);
            if (resModel.EmpID == 0)
            {
                mSISDViewModel.Modifyby = "Admin";
            }
            else
            { 
                mSISDViewModel.Modifyby = resModel.EmployeeName;
            }
            dal.unApprovedAssignedSimPartner(mSISDViewModel); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SimAssignPartnerCampus()
        { 
           return View(); 
        }
    }
}