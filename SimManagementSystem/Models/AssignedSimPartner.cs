using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class AssignedSimPartner
    {
        public int ID { get; set; }
        public string EmployeeDesiginationID { get; set; }
        public string VoicePackageID { get; set; }
        public string DataPackageID { get; set; }
        public string Approvedby { get; set; }
        public string OwnerName { get; set; } 
        public string CNICNumber { get; set; }
        public string EmployeePost { get; set; }
        public string ApprovedDate { get; set; }
        public string CreditLimit { get; set; }
        public string Modifyby { get; set; }
        public string Modifydate { get; set; } 
        public string Createby { get; set; }
        public string Createddate { get; set; }
        public string VoicePackageName { get; set; }
        public string DataPackage { get; set; }
        public string EmployeeShift { get; set; } 
        public string Remarks { get; set; }
        public string MobileNumber { get; set; }
    }
}