using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using Holf.AllForOne;

namespace BookServiceQA.Support_classes
{
    public class IisExpressWebServer
    {
        private static WebApplication _application;
        private static Process _webHostProcess;

        public IisExpressWebServer(WebApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("The web application must be set.");
            _application = application;
        }

        public void Start(string configTransform = null)
        {
            string iisconfig = @"C:\C#.Pluralsight\TrainingProject\testsite\BookServiceQA\applicationhost_local.config"; // To be parameterized!!
            ProcessStartInfo webHostStartInfo;
            if (configTransform == null)
            {
                webHostStartInfo = InitializeIisExpress(_application, iisconfig); //Lorenzo
                //webHostStartInfo = InitializeIisExpress(_application);
            }
            else
            {
                var siteDeployer = new MsBuildDeployer(_application.Location);
                var deployPath = Path.Combine(Environment.CurrentDirectory, "TestSite");
                siteDeployer.Deploy(configTransform, deployPath);
                webHostStartInfo = InitializeIisExpress(_application, iisconfig, deployPath);//Start iis from appconfig
                //webHostStartInfo = InitializeIisExpress(_application, deployPath);
            }
            _webHostProcess = Process.Start(webHostStartInfo);
            _webHostProcess.TieLifecycleToParentProcess();
        }

        public void Stop()
        {
            if (_webHostProcess == null)
                return;
            if (!_webHostProcess.HasExited)
                _webHostProcess.Kill();
            _webHostProcess.Dispose();
        }

        public string BaseUrl
        {
            get { return string.Format("http://localhost:{0}", _application.PortNumber); }
        }

        //private static ProcessStartInfo InitializeIisExpress(WebApplication application, string deployPath = null)
        private static ProcessStartInfo InitializeIisExpress(WebApplication application, string config, string deployPath = null)//Start iis from appconfig
        {
            // todo: grab stdout and/or stderr for logging purposes?
            var key = Environment.Is64BitOperatingSystem ? "programfiles(x86)" : "programfiles";
            var programfiles = Environment.GetEnvironmentVariable(key);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                //Arguments = String.Format("/path:\"{0}\" /port:{1}", deployPath ?? application.Location.FullPath, application.PortNumber),
                Arguments = string.Format("/config:\"{0}\" ", config),//Start iis from appconfig
                FileName = string.Format("{0}\\IIS Express\\iisexpress.exe", programfiles)
            };

            foreach (var variable in application.EnvironmentVariables)
                startInfo.EnvironmentVariables.Add(variable.Key, variable.Value);

            return startInfo;
        }

        public void ModifyConfig(WebApplication app)
        {
            var key = Environment.Is64BitOperatingSystem ? "programfiles(x86)" : "programfiles";
            var programfiles = Environment.GetEnvironmentVariable(key);
            // Modify the applicationhost_local.config to use the correct physical path for the website
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = string.Format("{0}\\IIS Express\\APPCMD.exe", programfiles);
            startInfo.Arguments = "/apphostconfig:\"" + app.Location.FullPath + "QA\\applicationhost_local.config\" SET site /site.name:\"Development Web Site\" /application[path='/'].virtualdirectory[path='/'].physicalPath:\"" + app.Location.FullPath + "\"";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
