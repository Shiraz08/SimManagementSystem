using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class ViewModelMenus
    {
        public bool isActive { get; set; }

        public DateTime MenuEditDate { get; set; }

        public string MenuDescription { get; set; }

        public string MenuName { get; set; }


        public long ID { get; set; }
    }
    public class Menu_ID 
    {
        public long MenuId { get; set; }
    }
}