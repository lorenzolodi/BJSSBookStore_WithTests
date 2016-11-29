using NUnit.Framework;
using BookServiceQA.Support_classes;

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
            // Modify the applicationhost_local.config to use the correct physical path for the website
            string strCmdText = "APPCMD /apphostconfig:\""+app.Location.FullPath+ "\\BookServiceQA\applicationhost_local.config\" SET site /site.name:\"Development Web Site\" /application[path='/'].virtualdirectory[path='/'].physicalPath:\"" + app.Location.FullPath + "\"";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
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
