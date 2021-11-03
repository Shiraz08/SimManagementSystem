using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SimManagementSystem.CommonUtility;
using SimManagementSystem.Controllers;
using SimManagementSystem.DataContext;
using SimManagementSystem.Models;
using static SimManagementSystem.Models.Dropdown;

namespace SimManagementSystem.DAL
{
    public class AssignedSimNewPartnerDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();

        public List<VoicePackageNameModel> GetVoicePackageName() 
        {
            List<VoicePackageNameModel> EvaluationParameters = new List<VoicePackageNameModel>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_Sim_Voice_Package_Name");
                    EvaluationParameters = new CommonFunction().GetObjectList<VoicePackageNameModel>(dt);
                } 
            }
            catch (Exception ex) 
            {

            }
            return EvaluationParameters;
        }
        public List<DataPackageNameModel> GetDataPackageName()
        {
            List<DataPackageNameModel> EvaluationParameters = new List<DataPackageNameModel>();
            try 
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_Sim_Data_Package_Name");
                    EvaluationParameters = new CommonFunction().GetObjectList<DataPackageNameModel>(dt);
                }
            } 
            catch (Exception ex)
            {

            }
            return EvaluationParameters;
        }
        public List<AssignedSimPartner> GetSimNewPartner()
        {
      
            List<AssignedSimPartner> list = new List<AssignedSimPartner>();
            try 
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_Get_Assigned_Sim_New_Partner");
                    list = new CommonFunction().GetObjectList<AssignedSimPartner>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int SaveDataAssignedSimPartner(AssignedSimPartner vm)
        {
            int res = 0;
            try
            {
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@OwnerName",vm.OwnerName),
                        new SqlParameter("@CNICNumber", vm.CNICNumber),
                         new SqlParameter("@ApprovedDate", null),
                        new SqlParameter("@EmployeePost", vm.EmployeePost),
                        new SqlParameter("@MobileNumber", vm.MobileNumber),
                        new SqlParameter("@CreditLimit", vm.CreditLimit),
                         new SqlParameter("@VoicePackageName", vm.VoicePackageName),
                         new SqlParameter("@Approvedby",vm.Approvedby),
                        new SqlParameter("@DataPackage",  vm.DataPackage),
                        new SqlParameter("@EmployeeShift", vm.EmployeeShift),
                        new SqlParameter("@Createdby", list.EmployeeName),
                        new SqlParameter("@Createddate", DateTime.Now.Date),
                    };
                    res = objAdo.ExecNonQueryProc("SP_Add_Assigned_Sim_New_Partner", parameters);
                }
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "SimNewPartner_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Add SimAssignedNewPartner")
                        };
                    res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                }
                UpdateNUmberStatus(vm.MobileNumber); 
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public void UpdateNUmberStatus(string MobileNumber)
        {
            SqlConnection con = new SqlConnection("Data Source=ips1.cyberisol.com;Initial Catalog=Unique;uid=unique;pwd=NMTL@hore19Ch@irm@n!@#$");
            SqlCommand cmd = new SqlCommand("update Sim_Tbl_MultipleNumber set IsAssign = 'Yes' where  TelNumber = @NUmber", con);
            cmd.Parameters.AddWithValue("@NUmber", MobileNumber);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public int EditDataAssignedSimPartner(AssignedSimPartner vm)
        {
            int res = 0;
            try
            {
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@OwnerName",vm.OwnerName),
                        new SqlParameter("@CNICNumber", vm.CNICNumber),
                        new SqlParameter("@EmployeePost", vm.EmployeePost),
                        new SqlParameter("@MobileNumber", vm.MobileNumber),
                        new SqlParameter("@CreditLimit", vm.CreditLimit),
                         new SqlParameter("@VoicePackageName", vm.VoicePackageName),
                        new SqlParameter("@DataPackage",  vm.DataPackage),
                        new SqlParameter("@EmployeeShift", vm.EmployeeShift),
                        new SqlParameter("@Modifydate", DateTime.Now.Date),
                        new SqlParameter("@Modifyby",list.EmployeeName),
                        new SqlParameter("@ID",vm.ID)
                    };
                    res = objAdo.ExecNonQueryProc("SP_Edit_Assigned_Sim_New_Partner", parameters);
                }
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        { new SqlParameter("@Form_Name", "SimNewPartner_Conroller"),
                        new SqlParameter("@UserId", val),
                           new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Edit SimAssignedNewPartner") };
                    res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int DeleteAssignedSimPartner(int id)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@ID", id)
                        };
                    res = objAdo.ExecNonQueryProc("SP_Delete_Assigned_Sim_New_Partner", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        { new SqlParameter("@Form_Name", "SimNewPartner_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Delete SimAssignedNewPartner") };
                    res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                }

            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int ApprovedAssignedSimPartner(AssignedSimPartner vm)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  
                        {
                        new SqlParameter("@ID", vm.ID) ,
                        new SqlParameter("@Approvedby", vm.Approvedby),
                        new SqlParameter("@ApprovedDate", DateTime.Now.Date),
                        new SqlParameter("@Remarks", vm.Remarks) 
                        };
                    res = objAdo.ExecNonQueryProc("SP_Approve_Assigned_Sim_New_Partner", parameters);
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        { new SqlParameter("@Form_Name", "SimNewPartner_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Approved Sim Assigned New Partner") };
                    res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                }

            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int unApprovedAssignedSimPartner(AssignedSimPartner vm)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        {
                        new SqlParameter("@ID", vm.ID) ,
                        new SqlParameter("@Modifyby", vm.Modifyby), 
                        new SqlParameter("@Modifydate", DateTime.Now)
                        };
                    res = objAdo.ExecNonQueryProc("SP_UNApprove_Assigned_Sim_New_Partner", parameters); 
                }
                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        { new SqlParameter("@Form_Name", "SimNewPartner_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Un Approved Sim Assigned New Partner") };
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