using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimManagementSystem.Controllers
{
    public class BillingController : Controller
    {
        WebHelper web = new WebHelper();
        BillingAmountDAL db = new BillingAmountDAL();
        SimMobileIssuanceDAL dbs = new SimMobileIssuanceDAL();
        SimMobileIssuanceDAL desigantion = new SimMobileIssuanceDAL();
        // GET: Billing
        public ActionResult Index()
        {
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>new SelectListItem(){ Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")", Value = x.ToString()}), "Value", "Text");
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x => new SelectListItem() {Text = x.ToString(),Value = x.ToString()}), "Value", "Text");
            return View();
        }
        public ActionResult Addmanualbilling()
        {
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x => new SelectListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1] + " (" + x + ")", Value = x.ToString() }), "Value", "Text");
            ViewBag.Years = new SelectList(Enumerable.Range(DateTime.Today.Year, 20).Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }), "Value", "Text");
            ViewBag.Designation = desigantion.GetDesignation().Select(x => new SelectListItem { Text = x.Designationname.ToString(), Value = x.Designationid.ToString() }).Distinct().ToList();
            ViewBag.Department = dbs.GetDepartment().Select(x => new SelectListItem { Text = x.Description.ToString(), Value = x.DepttID.ToString() }).Distinct().ToList();
            ViewBag.Campus = dbs.GetCampus().Select(x => new SelectListItem { Text = x.campusname.ToString(), Value = x.id.ToString() }).Distinct().ToList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveManualBilling(BillingFile dataToPost) 
        {
            var val = web.GetUserIdentityFromSession();
            dataToPost.Createby = val.Name;
            db.SaveBillingAmountList(dataToPost); 
            db.SaveHistory();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateArear(BillingFile dataToPost)  
        {
            var val = web.GetUserIdentityFromSession(); 
            dataToPost.Modifyby = val.Name;
            dataToPost.Modifydate =Convert.ToString(DateTime.Now);
            db.UpdateArear(dataToPost); 
            db.SaveHistory();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveBillingFile(BillingList dataToPost)  
        {
            var val = web.GetUserIdentityFromSession();
            foreach (var item in dataToPost.ImportBillingFile)
            {
                item.Createby = val.Name;
                db.SaveBillingAmountList(item);
            }
            db.SaveHistory();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBillingAmount() 
        {
            var val = web.GetUserIdentityFromSession();
            if (val.Name == "Admin")
            {
                var list = db.GetBillingAmountList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else 
            {
                var list = db.GetBillingAmountList().Where(x => x.Createby == val.Name).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Delete(BillingFile bf) 
        {
            db.DeleteBilling(bf.BillingId); 
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}