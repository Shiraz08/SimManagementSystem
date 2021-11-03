using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class HistoryViewModel
    {
        public long ID { get; set; }

        public string Form_Name { get; set; }

        public DateTime? Action_Date { get; set; }

        public int UserId { get; set; }

        public string User_Name { get; set; }

        public string Action_Name { get; set; }
    }
}