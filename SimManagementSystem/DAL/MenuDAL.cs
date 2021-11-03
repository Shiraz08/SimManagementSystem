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
    public class MenuDAL
    {
        WebHelper web = new WebHelper();
        UserDAL ud = new UserDAL();
        public int SaveAssigns(long UserID, int module)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@UserID", UserID),
                        new SqlParameter("@Module", module)

                        };
                    res = objAdo.ExecNonQueryProc("SP_AssignMenus_Insert", parameters);
                }

                var val = web.GetUserIdentityFromSession().UserId;
                var list = ud.GetEmployeeImage(val);
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@Form_Name", "Menu_Conroller"),
                        new SqlParameter("@UserId", val),
                        new SqlParameter("@User_Name", list.EmployeeName),
                        new SqlParameter("@Action_Name", "Assign Menu")
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
        public int DeleteById(long UserID, int module) 
        {
            int res = 0;
            try
            {  
                    using (AdoHelper objAdo = new AdoHelper())
                    {
                        SqlParameter[] parameters =  {
                        new SqlParameter("@UserID", UserID),
                        new SqlParameter("@Module", module)

                        }; 
                        res = objAdo.ExecNonQueryProc("SP_AssignMenus_Deletebyid", parameters);
                    }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public int DeleteAssigns(long UserID)
        {
            int res = 0;
            try
            {
                using (AdoHelper objAdo = new AdoHelper())
                {
                    SqlParameter[] parameters =  {
                        new SqlParameter("@UserID", UserID)

                        };
                    res = objAdo.ExecNonQueryProc("SP_AssignMenus_Delete", parameters);
                }
            }
            catch (Exception ex)
            {
                res = 0;
            }
            return res;
        }
        public List<Menu_ID> GetChecks(long UserID)
        {
            List<Menu_ID> list = new List<Menu_ID>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = "@UserID";
                    sqlParameter.Value = UserID;

                    DataTable dt = adoHelper.ExecDataTableProc("SP_Check_Module", sqlParameter);
                    list = new CommonFunction().GetObjectList<Menu_ID >(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public List<ViewModelMenus> GetMenusNames()
        {
            List<ViewModelMenus> list = new List<ViewModelMenus>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_MENUS");
                    list = new CommonFunction().GetObjectList<ViewModelMenus>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }



    }
}