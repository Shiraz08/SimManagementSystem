using SimManagementSystem.CommonUtility;
using SimManagementSystem.DAL;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimManagementSystem.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        UserDAL userdal = new UserDAL();
        MsisdDAL dbs = new MsisdDAL();
        UserViewModel user = new UserViewModel();
        AssignMSISDDAL db = new AssignMSISDDAL();
        MenuDAL menu = new MenuDAL();
        DataPackageNameDAL p = new DataPackageNameDAL();
        VoicePackageNameDAL v = new VoicePackageNameDAL();
        AssignedSimNewPartnerDAL si = new AssignedSimNewPartnerDAL(); 
        MobileNumberSeriesDAL series = new MobileNumberSeriesDAL();
        public ActionResult Index()
        {
            TableLayout tl = new TableLayout();
            tl.Sims = series.GetNumbers();
            tl.msisdviewmodel = dbs.GetMSISD();
            tl.assignmsisdviewmodel = db.GetAssignMSISD();
            tl.AssignMenu = menu.GetMenusNames();
            tl.assignedsimpartner = si.GetSimNewPartner();
            tl.datapackagenamemodel = p.GetDataPackageName();
            tl.voicepackagenamemodel = v.GetVoicePackageName();
            return View(tl); 
        }
        public System.Drawing.Image GetEmployeePicture(int EmpID)
        {
            EmployeeDetailVM res = new EmployeeDetailVM();
            res = userdal.GetEmployeeImage(EmpID); 
            System.Drawing.Image img = null;
            if (res.Picture != null)
            {
                MemoryStream ms = new MemoryStream(res.Picture);
                img = System.Drawing.Image.FromStream(ms);
                Response.ContentType = "image/gif";
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return img;
        }

    }
}