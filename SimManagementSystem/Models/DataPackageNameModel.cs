using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class DataPackageNameModel
    {
        public int DP_ID { get; set; }

        public string DP_Description { get; set; }

        public string DP_GB_Type { get; set; }

        public string DP_Price { get; set; }
        public string AddedBy { get; set; }

        public string AddedDate { get; set; }

        public string ModifiedBy { get; set; }
         
        public string ModifiedDate { get; set; }
    }
}