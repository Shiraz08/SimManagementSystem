using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class SimMobileIssuance
    {
        public int ID { get; set; }

        public string MobileNumber { get; set; }

        public int BtypeID { get; set; }
         
        public int DepartmentID { get; set; }

        public int CampusID { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCNIC { get; set; } 
        public string CreditLimit { get; set; }
        public string EmployeeDesiginationID { get; set; }
        public string VoicePackageName { get; set; }
        public string DataPackage { get; set; }
        public string EmployeeShift { get; set; }
        public string VoicePackageID { get; set; }
        public string DataPackageID { get; set; }
        public int EmployeeID { get; set; }

        public string AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public string ModifyBy { get; set; }

        public DateTime ModifyDate { get; set; }
    }
    public class SimMobileIssuanceViewModel
    {
        public string CreditLimit { get; set; }
        public int ID { get; set; }
        public int Designationid { get; set; }
        public string Designationname { get; set; }
        public int Btypeid { get; set; }
        public string Btype_Name { get; set; }
        public string MobileNumber { get; set; }
        public string VoicePackageName { get; set; }
        public string DataPackage { get; set; }
        public string EmployeeShift { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public int DepttID { get; set; }
        public string Description { get; set; }
        public int Campusid { get; set; }
        public string campusname { get; set; }
        public string EmployeeCNIC { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeDesiginationID { get; set; }
        public string VoicePackageID { get; set; }
        public string DataPackageID { get; set; }
    }
}