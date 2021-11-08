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
    public class MobileNumberSeriesDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public int Delete(long id)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@ID", id)
                        };
                    res = objAdo.ExecNonQueryProc("sp_Del_Numbers", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;

        }
        public List<SimInfo> GetNumbers()
        {
            List<SimInfo> list = new List<SimInfo>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GetNumbers");
                    list = new CommonFunction().GetObjectList<SimInfo>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveAssigns(SimInfo local)
        {
            var val = web.GetUserIdentityFromSession().UserId;
            var list = ud.GetEmployeeImage(val);
            int res = 0;
            try
            {
                long diff =Convert.ToInt64( local.to) - Convert.ToInt64(local.from);
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    if (diff >= 0)
                    {
                        for (long i = Convert.ToInt64(local.from); i <= Convert.ToInt64(local.to); i++)
                        {
                            SqlParameter[] locals = new SqlParameter[] { new SqlParameter("@ItNumberReference", System.Data.SqlDbType.NVarChar), new SqlParameter("@TelNetwork", System.Data.SqlDbType.NVarChar), new SqlParameter("@TelNumber", System.Data.SqlDbType.NVarChar), new SqlParameter("@CountNumber", System.Data.SqlDbType.BigInt), new SqlParameter("@CreatedBy", System.Data.SqlDbType.NVarChar) };
                            locals[0].Value = local.ItNumberReference; locals[1].Value = local.TelNetwork; locals[3].Value = diff;
                            locals[2].Value = i.ToString("00000000000.##"); 
                            locals[locals.Count() - 1].Value = list.EmployeeName;
                            try { adoHelper.ExecNonQueryProc("SimInfoOperation", locals); }
                            catch (Exception) { }
                        }
                    }
                }



                var vals = web.GetUserIdentityFromSession().UserId;
                var lists = ud.GetEmployeeImage(vals);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        {
                        new SqlParameter("@Form_Name", "MobileNumberSeries_Conroller"),
                        new SqlParameter("@UserId", vals),
                        new SqlParameter("@User_Name", lists.EmployeeName),
                        new SqlParameter("@Action_Name", "Get Sim(Save)")
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
        public int UpdateMSISD(SimInfo vm) 
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper()) 
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@ID", vm.ID),
                        new SqlParameter("@Sim_MSISD_No", vm.Sim_MSISD_No),
                         new SqlParameter("@ModifiedDate", vm.ModifiedDate),
                        new SqlParameter("@ModifiedBy", vm.ModifiedBy)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Edit_MSISD_Number", parameters);
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