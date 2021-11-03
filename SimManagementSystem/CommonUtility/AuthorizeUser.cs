
using SimManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimManagementSystem.CommonUtility
{
    public class CustomAuthorize : AuthorizeAttribute
    { 
       // public AuthSites Sites { get; set; }
        public bool AllowAnonymous { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool authorize = false;
            UserViewModel user = new UserViewModel();
            user = new WebHelper().GetUserIdentityFromSession();
            if (user == null)
            {
                // httpContext.Response.RedirectToRoute( new RouteValueDictionary{{ "controller", "User" }, { "action", "Login" }});
                RedirectToRouteResult routeData = null;
                var returnUrl = string.Empty;
                routeData = new RedirectToRouteResult(
                                    new RouteValueDictionary(new { controller = "User", action = "Login", returnUrl = returnUrl }));

            }
            else
            {
                authorize = true;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}