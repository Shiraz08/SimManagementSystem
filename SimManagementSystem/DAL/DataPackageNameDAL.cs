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
    public class DataPackageNameDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public List<DataPackageNameModel> GetDataPackageName()
        {
            List<DataPackageNameModel> list = new List<DataPackageNameModel>();
            try
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_Sim_Data_Package_Name");
                    list = new CommonFunction().GetObjectList<DataPackageNameModel>(dt);
                }
            }
            catch (Exception ex) 
            {

            }
            return list;
        }
        public int SaveDataPackageName(DataPackageNameModel vm)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@DP_Description", vm.DP_Description),
                        new SqlParameter("@DP_GB_Type", vm.DP_GB_Type),
                        new SqlParameter("@DP_Price", vm.DP_Price),
                        new SqlParameter("@AddedBy", vm.AddedBy),
                         new SqlParameter("@AddedDate", DateTime.Now)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Insert_Sim_Data_Package_Name", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "DataPackageName_Controller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Add Data Package Name")
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
        public int EditDataPackageName(DataPackageNameModel vm) 
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        {
                        new SqlParameter("@DP_ID", vm.DP_ID), 
                        new SqlParameter("@DP_Description", vm.DP_Description),
                        new SqlParameter("@DP_GB_Type", vm.DP_GB_Type),
                        new SqlParameter("@DP_Price", vm.DP_Price),
                        new SqlParameter("@AddedBy", vm.AddedBy),
                        new SqlParameter("@AddedDate", vm.AddedDate), 
                        new SqlParameter("@ModifiedBy", vm.ModifiedBy),
                        new SqlParameter("@ModifiedDate", DateTime.Now)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Update_Sim_Data_Package_Name", parameters);
                }

                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                { SqlParameter[] parameters = { new SqlParameter("@Form_Name", "DataPackageName_Conroller"), new SqlParameter("@UserId", val), new SqlParameter("@User_Name", list.EmployeeName), new SqlParameter("@Action_Name", "Edit Data Package Name") }; res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters); }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int DeleteDataPackageName(int id) 
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@DP_ID", id) 
                        };
                    res = objAdo.ExecNonQueryProc("SP_Delete_Sim_Data_Package_Name", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                { SqlParameter[] parameters = { new SqlParameter("@Form_Name", "DataPackageName_Conroller"), new SqlParameter("@UserId", val), new SqlParameter("@User_Name", list.EmployeeName), new SqlParameter("@Action_Name", "Delete Data Package Name") }; res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters); }

            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
    }
}