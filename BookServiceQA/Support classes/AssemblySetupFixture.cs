using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using BookService.Controllers;
using BookServiceQA.Support_classes;
using BookService.Models;
using OpenQA.Selenium;

namespace BookServiceQA
{
    [SetUpFixture]
    public class AssemblySetupFixture
    {
        public static IisExpressWebServer WebServer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var app = new WebApplication(ProjectLocation.FromFolder("BookService"), 44302);
            WebServer = new IisExpressWebServer(app);
            app.AddEnvironmentVariable("QA");
            WebServer.Start();

            Browser.Driver();

            //DbManager dbAccess = new DbManager();
            //dbAccess.ClearDB();
            //dbAccess.PopulateDB();
            //dbAccess.detach();

            //TestData dataBuilder = new TestData();
            //dataBuilder.DeleteAllBooks();
            //dataBuilder.DeleteAllAuthors();
            //dataBuilder.PopulateAuthors();
            //dataBuilder.PopulateBooks();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Browser.Driver().Quit();
        }
    }
}
