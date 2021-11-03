using SimManagementSystem.CommonUtility;
using SimManagementSystem.DataContext;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SimManagementSystem.DAL
{
    public class VoicePackageNameDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public List<VoicePackageNameModel> GetVoicePackageName()
        {
            List<VoicePackageNameModel> list = new List<VoicePackageNameModel>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_Sim_Voice_Package_Name");
                    list = new CommonFunction().GetObjectList<VoicePackageNameModel>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveVoicePackageName(VoicePackageNameModel vm)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@All_Network_Mints", vm.All_Network_Mints),
                        new SqlParameter("@All_Network_SMS", vm.All_Network_SMS ),
                        new SqlParameter("@All_Net_Data", vm.All_Net_Data ),
                        new SqlParameter("@Other_Network_Mints ", vm.Other_Network_Mints  ),
                         new SqlParameter("@Package_Description ", vm.Package_Description  ),
                        new SqlParameter("@Package_Tax_Description ", vm.Package_Tax_Description  ),
                        new SqlParameter("@AddedBy", vm.AddedBy),
                         new SqlParameter("@AddedDate", DateTime.Now)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Insert_Sim_Voice_Package_Name", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "VoicePackageName_Controller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Add Voice Package Name")
                        };
                    res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int EditVoicePackageName(VoicePackageNameModel vm) 
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters = 
                        {
                        new SqlParameter("@VPN_ID", vm.VPN_ID), 
                        new SqlParameter("@All_Network_Mints", vm.All_Network_Mints),
                        new SqlParameter("@All_Network_SMS", vm.All_Network_SMS ),
                        new SqlParameter("@All_Net_Data", vm.All_Net_Data ),
                        new SqlParameter("@Other_Network_Mints ", vm.Other_Network_Mints  ),
                        new SqlParameter("@Package_Description ", vm.Package_Description  ),
                        new SqlParameter("@Package_Tax_Description ", vm.Package_Tax_Description  ),
                        new SqlParameter("@AddedBy", vm.AddedBy),
                        new SqlParameter("@AddedDate", vm.AddedDate), 
                        new SqlParameter("@ModifiedBy", vm.ModifiedBy),
                        new SqlParameter("@ModifiedDate", DateTime.Now) 
                        };
                    res = objAdo.ExecNonQueryProc("SP_Update_Sim_Voice_Package_Name", parameters);
                }

                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                { SqlParameter[] parameters = { new SqlParameter("@Form_Name", "VoicePackageName_Conroller"), new SqlParameter("@UserId", val), new SqlParameter("@User_Name", list.EmployeeName), new SqlParameter("@Action_Name", "Edit Voice Package Name") }; res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters); }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int DeleteVoicePackageName(int id) 
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@VPN_ID", id) 
                        };
                    res = objAdo.ExecNonQueryProc("SP_Delete_Sim_Voice_Package_Name", parameters); 
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                { SqlParameter[] parameters = { new SqlParameter("@Form_Name", "VoicePackageName_Conroller"), new SqlParameter("@UserId", val), new SqlParameter("@User_Name", list.EmployeeName), new SqlParameter("@Action_Name", "Delete Voice Package Name") }; res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters); }

            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
    }
}