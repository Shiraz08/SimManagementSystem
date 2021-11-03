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
    public class MsisdDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public List<MSISDViewModel> GetMSISD() 
        {
            List<MSISDViewModel> list = new List<MSISDViewModel>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                { 
                    DataTable dt = adoHelper.ExecDataTableProc("SP_Get_MSISD"); 
                    list = new CommonFunction().GetObjectList<MSISDViewModel>(dt); 
                } 
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveMSISD(MSISDViewModel vm)
        {
            int res = 0;
            try
            { 
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@IT_Reference_No", vm.IT_Reference_No),
                        new SqlParameter("@Mobile_Network", vm.Mobile_Network),
                        new SqlParameter("@Sim_MSISD_No", vm.Sim_MSISD_No),
                        new SqlParameter("@Added_By", vm.Added_By),
                         new SqlParameter("@Added_Date", DateTime.Now),
                        new SqlParameter("@Modify_By", null), 
                        new SqlParameter("@Modify_Date", null)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Insert_MSISD", parameters); 
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "MSISD_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Add MSISD")
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
        public int EditMSISD(MSISDViewModel vm)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@ID", vm.ID), 
                        new SqlParameter("@IT_Reference_No", vm.IT_Reference_No),
                        new SqlParameter("@Mobile_Network", vm.Mobile_Network),
                        new SqlParameter("@Sim_MSISD_No", vm.Sim_MSISD_No),
                        new SqlParameter("@Added_By", vm.Added_By),
                         new SqlParameter("@Added_Date", DateTime.Now),
                        new SqlParameter("@Modify_By", vm.Modify_By),
                        new SqlParameter("@Modify_Date", DateTime.Now)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Update_MSISD", parameters);
                }

                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {SqlParameter[] parameters =  {new SqlParameter("@Form_Name", "MSISD_Conroller"),new SqlParameter("@UserId", val),new SqlParameter("@User_Name", list.EmployeeName),new SqlParameter("@Action_Name", "Edit MSISD")};res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);}
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int DeleteMSISD(int id)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@ID", id)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Delete_MSISD", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                { SqlParameter[] parameters = { new SqlParameter("@Form_Name", "MSISD_Conroller"), new SqlParameter("@UserId", val), new SqlParameter("@User_Name", list.EmployeeName), new SqlParameter("@Action_Name", "Delete MSISD") }; res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters); }

            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
    }
}