using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eblog.Admin.CustomFilter
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);
            var SessionControl = context.HttpContext.Session["yetki_id"];

            if (Convert.ToInt32(SessionControl) != 1)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Home" }, { "action", "Login" } });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}