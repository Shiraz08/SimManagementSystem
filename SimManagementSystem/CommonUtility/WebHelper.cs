using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SimManagementSystem.CommonUtility 
{
    public class WebHelper
    {
        public static string UserAccessKey
        {
            get
            {
                return "ifni_UserAccessKey";
            }
        }

        public void AddUserIdentityToSession(UserViewModel ue)
        {
            HttpContext.Current.Session.Add(UserAccessKey, ue);
        }
        public UserViewModel GetUserIdentityFromSession()
        {
            UserViewModel Ue;
            try
            {
                Ue =(UserViewModel)HttpContext.Current.Session[UserAccessKey];
            }
            catch(Exception Ex)
            {
                Ue = new UserViewModel();
            }

            if (Ue == null || Ue.UserId < 1)
                Ue = null;

            return Ue;
        }
        public void RemoveUserSession()
        {
            HttpContext.Current.Session.Remove(UserAccessKey);
        }
    }
}