using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimManagementSystem.Models
{
    public class VoicePackageNameModel
    {
        public int VPN_ID { get; set; }

        public string All_Network_Mints { get; set; }

        public string All_Network_SMS { get; set; }

        public string All_Net_Data { get; set; }

        public string Other_Network_Mints { get; set; }

        public string Package_Description { get; set; }

        public string Package_Tax_Description { get; set; }
        public string AddedBy { get; set; }

        public string AddedDate { get; set; }

        public string ModifiedBy { get; set; }
         
        public string ModifiedDate { get; set; }
    }
}