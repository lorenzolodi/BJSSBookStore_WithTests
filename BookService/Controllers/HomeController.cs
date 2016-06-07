using BookService.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "BJSS Book Store";

            return View();
        }

        [AuthFilterAttribute]
        public ActionResult Login(string username, string password)
        {
            ViewBag.Title = "BJSS Book Store";

            if (username.ToLower().Replace(" ", "") == "'=''or1=1" && password.ToLower().Replace(" ", "") == "'=''or1=1")
            {
                FormsAuthentication.SetAuthCookie("welldone", true);
            }

            return RedirectToAction("Index");
        }
    }
}
