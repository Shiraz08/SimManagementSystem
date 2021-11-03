using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class HistoryController : Controller
    {
        HistoryDAL db = new HistoryDAL();
        public ActionResult AddMSISDNoHistory()
        {
            var list = db.GetHistory().ToList(); 
            return View(list);
        }
    }
}