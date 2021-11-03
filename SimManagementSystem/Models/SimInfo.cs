using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class SimInfo
    {
        public string from { get; set; }

        public string to { get; set; }


        public long ID { get; set; }


        public string ItNumberReference { get; set; }


        public string TelNetwork { get; set; }

        public string TelNumber { get; set; }

        public long CountNumber { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public string Modifieddates { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}