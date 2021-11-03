using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class Dropdown
    {
        public class Designation
        {
            public int Designationid { get; set; }
            public string Designationname { get; set; }
        }
        public class Btype
        {
            public int ID { get; set; }
            public string Btype_Name { get; set; } 
        }
        public class MobileNumbers 
        {
            public string MobileNumber { get; set; }
            public string IsAssign  { get; set; }
        }
        public class Department
        {
            public int DepttID { get; set; }
            public string Description { get; set; }
        }
        public class Campus
        { 
            public int id { get; set; }
            public string campusname { get; set; }
        }
        public class EmployeeData
        {
            public int EMPID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class EmployeeCNIC 
        {
            public string CNIC { get; set; } 
            public string Designation { get; set; }
        }
    }
}