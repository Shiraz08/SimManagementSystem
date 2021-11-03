using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class BillingFile
    {
        public int BillingId { get; set; }
        public string Sn { get; set; }
        public string UserName { get; set; }
        public string Designation { get; set; }
        public string Campus_Department { get; set; }
        public string SimNumber { get; set; }
        public string UGILimit { get; set; }
        public string TotalAmount { get; set; }
        public string PayableByUGI { get; set; }
        public string Arears { get; set; }
        public string Months { get; set; }
        public string Years { get; set; }
        public string Modifyby { get; set; }
        public string Modifydate { get; set; }
        public string Createby { get; set; }
        public string Createddate { get; set; }
    }
    public class BillingList
    {
        public List<BillingFile> ImportBillingFile  { get; set; }
    }
}