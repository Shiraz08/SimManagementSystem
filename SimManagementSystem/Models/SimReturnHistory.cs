using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class SimReturnHistory
    {
        public int SimHistoryId { get; set; }

        public string EmployeeName { get; set; }

        public string SimAssignDate { get; set; }

        public string SimAssignBY { get; set; }

        public string DataPackage { get; set; }

        public string VoicePackage { get; set; }

        public string EmployeeShift { get; set; }

        public string CreditLimit { get; set; }

        public string EmployeeID { get; set; }

        public string CampusID { get; set; }

        public string DepartmentID { get; set; }

        public string BtypeID { get; set; }

        public string MobileNumber { get; set; }

        public string IsReturn { get; set; }
    }
}