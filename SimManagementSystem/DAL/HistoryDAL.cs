using SimManagementSystem.CommonUtility;
using SimManagementSystem.DataContext;
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SimManagementSystem.DAL
{
    public class HistoryDAL
    {
        public List<HistoryViewModel> GetHistory()
        {
            List<HistoryViewModel> list = new List<HistoryViewModel>();
            try
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    DataTable dt = adoHelper.ExecDataTableProc("SP_GET_History"); 
                    list = new CommonFunction().GetObjectList<HistoryViewModel>(dt);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
    }
}