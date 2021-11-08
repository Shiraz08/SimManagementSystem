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
    public class BillingAmountDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public List<BillingFile> GetBillingAmountList() 
        {
            List<BillingFile> list = new List<BillingFile>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("Sp_Sim_Get_BillingAmount"); 
                    list = new CommonFunction().GetObjectList<BillingFile>(dt); 
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveBillingAmountList(BillingFile vm)
        {
            int res = 0;
            try 
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Sn", 1),
                        new SqlParameter("@UserName", vm.UserName),
                        new SqlParameter("@Designation", vm.Designation.Trim()),
                        new SqlParameter("@Campus_Department", vm.Campus_Department.Trim()),
                        new SqlParameter("@SimNumber", vm.SimNumber),
                        new SqlParameter("@UGILimit", vm.UGILimit),
                        new SqlParameter("@TotalAmount", vm.TotalAmount),
                        new SqlParameter("@PayableByUGI", vm.PayableByUGI),
                        new SqlParameter("@Arears", vm.Arears),
                        new SqlParameter("@Months", vm.Months),
                        new SqlParameter("@Years", vm.Years),
                        new SqlParameter("@Createby", vm.Createby),
                        new SqlParameter("@Createddate", DateTime.Now),
                        new SqlParameter("@Modifyby", null),
                        new SqlParameter("@Modifydate", null)
                        };
                    res = objAdo.ExecNonQueryProc("Sp_Sim_Insert_BillingAmount", parameters);
                } 
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int SaveHistory()
        {
            int res = 0;
            try
            {
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "Billing_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Import Billing Amount File")
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
        public int DeleteBilling(int BillingId)  
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {new SqlParameter("@BillingId", BillingId) };
                    res = objAdo.ExecNonQueryProc("Sp_Sim_Delete_BillingAmount", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "Billing_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Delete Billing Amount")
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
    }
}