using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class TableLayout
    {
        public IEnumerable<MSISDViewModel>  msisdviewmodel { get; set; }
        public IEnumerable<AssignMSISDViewModel> assignmsisdviewmodel  { get; set; }
        public IEnumerable<ViewModelMenus> AssignMenu { get; set; }
        public IEnumerable<SimInfo> Sims { get; set; }
        public IEnumerable<VoicePackageNameModel> voicepackagenamemodel { get; set; } 
        public IEnumerable<DataPackageNameModel> datapackagenamemodel { get; set; } 
        public IEnumerable<AssignedSimPartner> assignedsimpartner { get; set; } 
         

         
    }
}