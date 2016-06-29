using System;
using System.Web.Http;
using System.Web.Mvc;
using BookService.Areas.HelpPage.ModelDescriptions;
using BookService.Areas.HelpPage.Models;
using BookService.Filters;

namespace BookService.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";
        public static Guid EnvId;

        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (id.HasValue)
                {
                    ViewBag.EnvironmentId = id.Value;
                    EnvId = id.Value;
                }
                //TODO: errors if user not authenticated and no guid
            }
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            //TODO: errors if user not authenticated and no guid
            ViewBag.EnvironmentId = EnvId;
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            //TODO: errors if user not authenticated and no guid
            ViewBag.EnvironmentId = EnvId;
            if (!String.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            return View(ErrorViewName);
        }
    }
}