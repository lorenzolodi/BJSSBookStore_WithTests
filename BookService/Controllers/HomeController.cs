using BookService.Filters;
using BookService.Models;
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
        private BookServiceContext db = new BookServiceContext();

        public ActionResult Environment(Guid id)
        {
            ViewBag.Title = "BJSS Book Store";
            ViewBag.EnvironmentId = id;

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewEnvironment()
        {
            var env = new Models.Environment();

            var jane = new Author() { Id = 1, Name = "Jane Austen", Environment = env };
            var charles = new Author() { Id = 2, Name = "Charles Dickens", Environment = env };
            var miguel = new Author() { Id = 3, Name = "Miguel de Cervantes", Environment = env };

            db.Books.AddRange(new[] {
                new Book()
                {
                    Id = 1,
                    Title = "Pride and Prejudice",
                    Year = 1813,
                    Author = jane,
                    Price = 9.99M,
                    Genre = "Commedy of manners",
                    Environment = env
                },
                new Book()
                {
                    Id = 2,
                    Title = "Northanger Abbey",
                    Year = 1817,
                    Author = jane,
                    Price = 12.95M,
                    Genre = "Gothic parody",
                    Environment = env
                },
                new Book()
                {
                    Id = 3,
                    Title = "David Copperfield",
                    Year = 1850,
                    Author = charles,
                    Price = 15,
                    Genre = "Bildungsroman",
                    Environment = env
                },
                new Book()
                {
                    Id = 4,
                    Title = "Don Quixote",
                    Year = 1617,
                    Author = miguel,
                    Price = 8.95M,
                    Genre = "Picaresque",
                    Environment = env
                }
            });

            db.SaveChanges();

            return RedirectToAction("Environment", new { id = env.Id });
        }

        public ActionResult Login(string username, string password, Guid? environmentId)
        {
            ViewBag.Title = "BJSS Book Store";

            if (username.ToLower().Replace(" ", "") == "'=''or1=1" && password.ToLower().Replace(" ", "") == "'=''or1=1")
            {
                FormsAuthentication.SetAuthCookie("welldone", true);
            }

            return environmentId.HasValue ? 
                RedirectToAction("Environment", new { Id = environmentId.Value }) : 
                RedirectToAction("Index");
        }
    }
}
