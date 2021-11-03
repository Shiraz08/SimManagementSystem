using SimManagementSystem.Models;
using SimManagementSystem.DBContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SimManagementSystem.DataContext;
using SimManagementSystem.CommonUtility;

namespace SimManagementSystem.DAL 
{
    public class UserDAL
    {
        CommonFunction commonFunction = new CommonFunction();
        public UserViewModel GetUser(UserViewModel model)
        {
            UserViewModel objuser = new UserViewModel();
            try
            {
                DataTable dtUser = new DataTable();
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@UserId", model.UserId),
                        new SqlParameter("@Password", model.Password),
                        };
                    dtUser = objAdo.ExecDataTableProc("usp_SMS_GetUser", parameters); 
                    if (new CommonFunction().isDataTableContainRecords(dtUser))
                    {
                        objuser = new CommonFunction().ConvertDataTable<UserViewModel>(dtUser).FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objuser;
        }
        public int ChangePassword(ChangePassword model)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@UserId", model.UserId),
                        new SqlParameter("@NewPassword", model.NewPassword),
                        };
                    res = objAdo.ExecNonQueryProc("usp_SMS_UpdatePassword", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int SendOTPtoUSer(string MobileNo, string otp_number)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@MobileNo", MobileNo),
                        new SqlParameter("@Code", otp_number)
                        };
                    res = objAdo.ExecNonQueryProc("USP_SendOTP", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public class IsValidOTP
        {
            public string OTP_Number { get; set; }
            public int UserID { get; set; }
            public DateTime Expired_Datetime { get; set; }
        }
        public List<IsValidOTP> GetUserINfo(string Userid)
        {
            List<IsValidOTP> list = new List<IsValidOTP>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                     new SqlParameter("@Userid", Convert.ToInt32(Userid))
                    };
                    DataTable dt = adoHelper.ExecDataTableProc("SP_SMS_GetUserINfo", parameters); 
                    list = commonFunction.GetObjectList<IsValidOTP>(dt);

                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveOTP(string userid, string otp_number)
        {
            SqlConnection con = new SqlConnection("Data Source=ips1.cyberisol.com;Initial Catalog=Unique;uid=unique;pwd=NMTL@hore19Ch@irm@n!@#$");
            int USerid = Convert.ToInt32(userid);
            string query = "insert into SMS_TblUser_OTP(UserID,OTP_Number,Expired_Datetime) values (@UserId , @otp_number, @DAte)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", USerid);
            cmd.Parameters.AddWithValue("@otp_number", otp_number);
            cmd.Parameters.AddWithValue("@DAte", DateTime.Now);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        } 
        public int SavenewPassword(string newpassword, string MobileNo)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@MobileNo", MobileNo),
                        new SqlParameter("@newpassword", newpassword)
                        };
                    res = objAdo.ExecNonQueryProc("USP_Updatepassword", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public string GetUSerID(string MobileNo)
        { 
            string res = "";
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@MobileNo", MobileNo)
                        };
                    res = Convert.ToString(objAdo.ExecScalarProc("USP_GetUSerID", parameters));
                }
            }
            catch (Exception ex)
            {
                res = "";
            }
            return res;
        }
        public string GetMobileNo(int UserId)
        {
            string res = "";
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@UserId", UserId)
                        };
                    res = Convert.ToString(objAdo.ExecScalarProc("USP_SMS_GetUSerID", parameters)); 
                }
            }
            catch (Exception ex)
            {
                res = "";
            }
            return res;
        }
        public EmployeeDetailVM GetEmployeeImage(int EmpID)
        {
            DataTable dt = new DataTable();
            EmployeeDetailVM obj = new EmployeeDetailVM();
            try
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                     new SqlParameter("@empid", EmpID)
                    };
                    dt = adoHelper.ExecDataTableProc("S_LoadEmployeeDataByID", parameters);
                    if (new CommonFunction().isDataTableContainRecords(dt))
                    {
                        obj = new CommonFunction().ConvertDataTable<EmployeeDetailVM>(dt).FirstOrDefault();
                    }
                    else
                    {
                        obj.EmployeeName = "Admin";
                        obj.EmpID = EmpID;       
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return obj;
        }
        public List<EmployeeData> GetEmployeeInfo() 
        {
            DataTable dt = new DataTable();
            List<EmployeeData> obj = new List<EmployeeData>();
            try
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    dt = adoHelper.ExecDataTableProc("S_LoadEmployeeData");
                    if (new CommonFunction().isDataTableContainRecords(dt))
                    {
                        obj = new CommonFunction().ConvertDataTable<EmployeeData>(dt);  
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return obj;
        }
    }
}