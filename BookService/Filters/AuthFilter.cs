using BookService.Extensions;
using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookService.Filters
{
    public class AuthFilterAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var environmentId = actionContext.Request.GetUserAccessToken();

            using (var db = new BookServiceContext())
            {
                var environment = db.Environments.Find(environmentId);
                if (environment == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.Forbidden);
                    var host = actionContext.Request.RequestUri.DnsSafeHost;
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Headers.Add("NoStore", string.Format("No store \"{0}\"", environmentId));
                }
            }
                        
            base.OnAuthorization(actionContext);            
        }
    }
}