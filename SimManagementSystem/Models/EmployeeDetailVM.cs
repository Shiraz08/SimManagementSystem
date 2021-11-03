using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{ 
    public class EmployeeDetailVM
    {
        public int EmpID { get; set; }
        public string EmployeeName { get; set; }
        public string DOJ { get; set; }
        public string CDOJ { get; set; }
        public Byte[] Picture { get; set; }
        public string Designation { get; set; }
        public string PostName { get; set; }
    }
    public class EmployeeData
    {
        public int EMPID { get; set; }
        public string EmployeeName { get; set; }
    }
}