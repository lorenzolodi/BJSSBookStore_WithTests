﻿using BookService.Filters;
using BookService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BookService.Extensions;

namespace BookService.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BookServiceContext db = new BookServiceContext();

        [AllowAnonymous]
        public ActionResult Environment(Guid id)
        {
            ViewBag.Title = "BJSS Book Store";
            ViewBag.EnvironmentId = id;
            

            if (db.Environments.Find(id) == null)
            {
                return View("NotFound");
            }

            ViewBag.Injected = db.Environments.Find(id).SqlInjected;

            return View();
        }

        [Authorize]
        public ActionResult Index()
        {
            
            return View(db.Environments.ToList());
        }

        [Authorize]
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

        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {

            try
            {
                Guid EnvironmentId;
                string token = Request.Headers.GetValues("x-user-token").FirstOrDefault();
                if (Guid.TryParse(token, out EnvironmentId))
                {

                    if (username.ToLower().Replace(" ", "") == "'=''or1=1" && password.ToLower().Replace(" ", "") == "'=''or1=1")
                    {
                        BookService.Models.Environment e = db.Environments.Find(EnvironmentId);
                        e.SqlInjected = true;
                        db.SaveChanges();
                        return PartialView("LoginBroken");
                    }
                }
            }
            catch { }

            return PartialView("Login");
        }
    }
}
