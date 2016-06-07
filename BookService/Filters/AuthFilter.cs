using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookService.Filters
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //get authentication token from header
            string authenticationToken = string.Empty;
            try
            {
                if (actionContext.Request.Headers.GetValues("x-user-token") != null && (!string.IsNullOrEmpty(Convert.ToString(actionContext.Request.Headers.GetValues("x-user-token").FirstOrDefault()))))
                {
                    authenticationToken = Convert.ToString(actionContext.Request.Headers.GetValues("x-user-token").FirstOrDefault());
                    if (authenticationToken != null)
                    {
                        
                        try
                        {
                            if (authenticationToken == "1234")
                            {
                                base.OnAuthorization(actionContext);
                            }
                            else
                            {
                                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                                HttpContext.Current.Response.AddHeader("ErrorMessage", "Forbidden - token incorrect");
                                return;
                            }
                        }
                        catch
                        {
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                            HttpContext.Current.Response.AddHeader("ErrorMessage", "Forbidden");
                            return;
                        }

                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                        HttpContext.Current.Response.AddHeader("ErrorMessage", "Authentication failed");
                        return;
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                    HttpContext.Current.Response.AddHeader("ErrorMessage", "Authentication precondition stage 2 failed");
                    return;
                }
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.PreconditionFailed);
                HttpContext.Current.Response.AddHeader("ErrorMessage", "Authentication precondition failed - " + ex.Message);
                return;
            }
        }
    }
}