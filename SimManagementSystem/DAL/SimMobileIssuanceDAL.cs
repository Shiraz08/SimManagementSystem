using SimManagementSystem.CommonUtility;
using SimManagementSystem.DataContext;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static SimManagementSystem.Models.Dropdown;

namespace SimManagementSystem.DAL
{
    public class SimMobileIssuanceDAL
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
                    res = objAdo.ExecNonQueryProc("Sp_Delete_MobileNumberIssuance", parameters);
                }

                var vals = web.GetUserIdentityFromSession().UserId;
                var lists = ud.GetEmployeeImage(vals);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =
                        {
                        new SqlParameter("@Form_Name", "SimMobileIssuance_Conroller"),
                        new SqlParameter("@UserId", vals),
                        new SqlParameter("@User_Name", lists.EmployeeName),
                        new SqlParameter("@Action_Name", "Delete_SimMobileIssuance")
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
        public List<SimMobileIssuanceViewModel> GetSimMobileIssuanceData()
        {
            List<SimMobileIssuanceViewModel> list = new List<SimMobileIssuanceViewModel>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("Sp_Get_MobileNumberIssuance");
                    list = new CommonFunction().GetObjectList<SimMobileIssuanceViewModel>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public List<SimReturnHistory> GetReturnSimData()
        {
            List<SimReturnHistory> list = new List<SimReturnHistory>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("Sp_Get_MobileNumberIssuance"); 
                    list = new CommonFunction().GetObjectList<SimReturnHistory>(dt); 
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public int EditSimMobileIssuance(SimMobileIssuanceViewModel local)
        {
            var val = web.GetUserIdentityFromSession().UserId;
            var list = ud.GetEmployeeImage(val);
            int res = 0;
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    SqlParameter[] locals = new SqlParameter[] {
                    new SqlParameter("@BtypeID", System.Data.SqlDbType.Int),
                    new SqlParameter("@MobileNumber", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int),
                    new SqlParameter("@CampusID", System.Data.SqlDbType.Int),
                    new SqlParameter("@AddedBy", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@AddedDate", System.Data.SqlDbType.DateTime),
                    new SqlParameter("@ModifyBy", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@ModifyDate", System.Data.SqlDbType.DateTime),
                    new SqlParameter("@EmployeeCNIC", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@EmployeeName", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int),
                    new SqlParameter("@EmployeeDesiginationID", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@ID", System.Data.SqlDbType.Int),
                    new SqlParameter("@DataPackage",  System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@EmployeeShift", System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@CreditLimit",System.Data.SqlDbType.NVarChar),
                    new SqlParameter("@VoicePackageName",System.Data.SqlDbType.NVarChar),
                };
                    locals[0].Value = local.Btypeid; locals[1].Value = local.MobileNumber; locals[2].Value = local.DepttID; locals[3].Value = local.Campusid; locals[4].Value = list.EmployeeName;
                    locals[5].Value = DateTime.Now.Date; locals[6].Value = list.EmployeeName;
                    locals[7].Value = DateTime.Now.Date;
                    locals[11].Value = local.EmployeeDesiginationID;
                    locals[8].Value = local.EmployeeCNIC;
                    locals[9].Value = local.EmployeeName.Trim();
                    locals[10].Value = local.EmployeeID;
                    locals[12].Value = local.ID;
                    locals[13].Value = local.DataPackage;
                    locals[14].Value = local.EmployeeShift;
                    locals[15].Value = local.CreditLimit;
                    locals[16].Value = local.VoicePackageName;
                    adoHelper.ExecNonQueryProc("Sp_Update_MobileNumberIssuance", locals);
                }
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "SimMobileIssuanceController"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "SaveEdit")
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
        public int SaveSimMobileIssuance(SimMobileIssuance local)
        {
            var val = web.GetUserIdentityFromSession().UserId;
            var list = ud.GetEmployeeImage(val);
            int res = 0;
            try
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    SqlParameter[] locals = new SqlParameter[]
                    {
                        new SqlParameter("@BtypeID", System.Data.SqlDbType.Int),
                        new SqlParameter("@MobileNumber", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@DepartmentID", System.Data.SqlDbType.Int),
                        new SqlParameter("@CampusID", System.Data.SqlDbType.Int),
                        new SqlParameter("@EmployeeName", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@AddedBy", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@AddedDate", System.Data.SqlDbType.DateTime),
                        new SqlParameter("@ModifyBy", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@EmployeeCNIC", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@EmployeeID", System.Data.SqlDbType.Int),
                        new SqlParameter("@EmployeeDesiginationID",System.Data.SqlDbType.NVarChar), 
                        new SqlParameter("@DataPackage",  System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@EmployeeShift", System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@CreditLimit",System.Data.SqlDbType.NVarChar),
                        new SqlParameter("@VoicePackageName",System.Data.SqlDbType.NVarChar), 
                    };
                    locals[0].Value = local.BtypeID;
                    locals[1].Value = local.MobileNumber;
                    locals[2].Value = local.DepartmentID;
                    locals[3].Value = local.CampusID;
                    locals[4].Value = local.EmployeeName;
                    locals[5].Value = list.EmployeeName;
                    locals[6].Value = DateTime.Now.Date;
                    locals[7].Value = "";
                    locals[8].Value = local.EmployeeCNIC;
                    locals[9].Value = local.EmployeeID;
                    locals[10].Value = local.EmployeeDesiginationID;
                    locals[11].Value = local.DataPackage;
                    locals[12].Value = local.EmployeeShift;
                    locals[13].Value = local.CreditLimit; 
                    locals[14].Value = local.VoicePackageName;
                    adoHelper.ExecNonQueryProc("Sp_Add_MobileNumberIssuance", locals);
                    //Add Sim History
                    var vals = web.GetUserIdentityFromSession().UserId;
                    var lists = ud.GetEmployeeImage(vals);
                    using (AdoHelper objAdo = new AdoHelper())
                    {
                        SqlParameter[] parameters =
                            {
                           new SqlParameter("@EmployeeName", local.EmployeeName),
                           new SqlParameter("@SimAssignDate",Convert.ToString(DateTime.Now)),
                           new SqlParameter("@SimAssignBY", list.EmployeeName),
                           new SqlParameter("@DataPackage", local.DataPackage),
                           new SqlParameter("@VoicePackage", local.VoicePackageName),
                           new SqlParameter("@EmployeeShift", local.EmployeeShift),
                           new SqlParameter("@CreditLimit", local.CreditLimit),
                           new SqlParameter("@EmployeeID", local.EmployeeID),
                           new SqlParameter("@CampusID", local.CampusID),
                           new SqlParameter("@DepartmentID", local.DepartmentID),
                           new SqlParameter("@BtypeID", local.BtypeID),
                           new SqlParameter("@MobileNumber", local.MobileNumber),
                           new SqlParameter("@IsReturn", null)
                        };
                        res = objAdo.ExecNonQueryProc("Sp_Add_SimHistory", parameters);
                    }
                    //Add Log
                    using (AdoHelper objAdo = new AdoHelper())
                    {
                        SqlParameter[] parameters =
                            {
                        new SqlParameter("@Form_Name", "MobileNumberSeries_Conroller"),
                        new SqlParameter("@UserId", vals),
                        new SqlParameter("@User_Name",  list.EmployeeName),
                        new SqlParameter("@Action_Name", "SAve_SimMobileIssuance")
                        };
                        res = objAdo.ExecNonQueryProc("SP_Insert_History", parameters);
                    }
                    //Update Number Status
                    UpdateNUmberStatus(local.MobileNumber);
                }
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
        public int UpdateReturnNUmberStatus(SimMobileIssuanceViewModel local) 
        {
            SqlConnection con = new SqlConnection("Data Source=ips1.cyberisol.com;Initial Catalog=Unique;uid=unique;pwd=NMTL@hore19Ch@irm@n!@#$");
            SqlCommand cmd = new SqlCommand("update Sim_Tbl_MultipleNumber set IsAssign = NULL where  TelNumber = @NUmber", con);
            cmd.Parameters.AddWithValue("@NUmber", local.MobileNumber);
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
            DeleteEnteryagainstreturmsimforoldcampus(local.MobileNumber);
            DeleteEnteryagainstreturmsimfornewcampus(local.MobileNumber); 
            return 1;
        }
        public int DeleteEnteryagainstreturmsimforoldcampus(string MobileNumber)
        {
            SqlConnection con = new SqlConnection("Data Source=ips1.cyberisol.com;Initial Catalog=Unique;uid=unique;pwd=NMTL@hore19Ch@irm@n!@#$");
            SqlCommand cmd = new SqlCommand("DELETE Sim_Tbl_SimMobileIssuance where MobileNumber = @NUmber", con);
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
            return 1;
        }
        public int DeleteEnteryagainstreturmsimfornewcampus(string MobileNumber)
        {
            SqlConnection con = new SqlConnection("Data Source=ips1.cyberisol.com;Initial Catalog=Unique;uid=unique;pwd=NMTL@hore19Ch@irm@n!@#$");
            SqlCommand cmd = new SqlCommand("DELETE Tbl_Create_Assigned_Sim_New_Partner where MobileNumber = @NUmber", con); 
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
            return 1;
        }
        public List<Designation> GetDesignation()
        {
            List<Designation> EvaluationParameters = new List<Designation>();
            DataTable dt = new AdoHelper().ExecDataTable("SELECT d.Designationid, d.Designationname FROM Designation d WHERE d.isActive = 1");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    Designation eval = new Designation();
                    eval.Designationid = Convert.ToInt32(DR["Designationid"]);
                    eval.Designationname = DR["Designationname"].ToString();
                    EvaluationParameters.Add(eval);
                }
            }

            return EvaluationParameters;
        }
        public List<Btype> GetBType()
        {
            List<Btype> EvaluationParameters = new List<Btype>();
            DataTable dt = new AdoHelper().ExecDataTable("SELECT ID, Btype_Name from Tbl_Btype");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    Btype eval = new Btype();
                    eval.ID = Convert.ToInt32(DR["ID"]);
                    eval.Btype_Name = DR["Btype_Name"].ToString();
                    EvaluationParameters.Add(eval);
                }
            }

            return EvaluationParameters;
        }
        public List<MobileNumbers> GetMobileNumber()
        {
            List<MobileNumbers> EvaluationParameters = new List<MobileNumbers>();
            DataTable dt = new AdoHelper().ExecDataTable("select TelNumber, IsAssign  from Sim_Tbl_MultipleNumber  where IsAssign is null");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    MobileNumbers eval = new MobileNumbers();
                    eval.MobileNumber = DR["TelNumber"].ToString();
                    eval.IsAssign = DR["IsAssign"].ToString(); 
                    EvaluationParameters.Add(eval);
                } 
            }
            return EvaluationParameters;
        }
        public List<MobileNumbers> GetAssignMobileNumber()
        {
            List<MobileNumbers> EvaluationParameters = new List<MobileNumbers>();
            DataTable dt = new AdoHelper().ExecDataTable("select TelNumber,IsAssign  from Sim_Tbl_MultipleNumber where IsAssign = 'Yes'");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    MobileNumbers eval = new MobileNumbers();
                    eval.MobileNumber = DR["TelNumber"].ToString();
                    eval.IsAssign = DR["IsAssign"].ToString();
                    EvaluationParameters.Add(eval);
                }
            }
            return EvaluationParameters;
        }
        public List<Department> GetDepartment()
        {
            List<Department> EvaluationParameters = new List<Department>();
            DataTable dt = new AdoHelper().ExecDataTable("select DepttID, Description from tblDepartment where isActive=1 and Description IS Not NULL ");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    Department eval = new Department();
                    eval.DepttID = Convert.ToInt32(DR["DepttID"]);
                    eval.Description = DR["Description"].ToString();
                    EvaluationParameters.Add(eval);
                }
            }
            return EvaluationParameters;
        }
        public List<Campus> GetCampus()
        {
            List<Campus> EvaluationParameters = new List<Campus>();
            DataTable dt = new AdoHelper().ExecDataTable("SELECT  id,campusname + ' ' +'(' + Building + ')' FROM school where ActiveSChool = 1");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    Campus eval = new Campus();
                    eval.id = Convert.ToInt32(DR["id"]);
                    eval.campusname = DR["campusname"].ToString();
                    EvaluationParameters.Add(eval);
                }
            }
            return EvaluationParameters;
        }
        public List<Models.EmployeeData> GetEmployeeInfo()
        {
            DataTable dt = new DataTable();
            List<Models.EmployeeData> obj = new List<Models.EmployeeData>();
            try
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    dt = adoHelper.ExecDataTableProc("S_LoadEmployeeData");
                    if (new CommonFunction().isDataTableContainRecords(dt))
                    {
                        obj = new CommonFunction().ConvertDataTable<Models.EmployeeData>(dt);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return obj;
        }
        public List<EmployeeCNIC> GetEmployeeCNIC(string value)
        {
            var _val = Convert.ToInt32(value);
            List<EmployeeCNIC> EvaluationParameters = new List<EmployeeCNIC>();
            DataTable dt = new AdoHelper().ExecDataTable("select NIC,Designation from V_EmployeeAllData WHERE  shift ='Morning' and EMPID =" + _val);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow DR in dt.Rows)
                {
                    EmployeeCNIC eval = new EmployeeCNIC();
                    eval.CNIC = DR["NIC"].ToString();
                    eval.Designation = DR["Designation"].ToString(); 
                    EvaluationParameters.Add(eval);
                }
            }
            return EvaluationParameters;
        }
    }
}