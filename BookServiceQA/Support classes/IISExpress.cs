﻿using System;
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
            ProcessStartInfo webHostStartInfo;
            if (configTransform == null)
            {
                webHostStartInfo = InitializeIisExpress(_application);
            }
            else
            {
                var siteDeployer = new MsBuildDeployer(_application.Location);
                var deployPath = Path.Combine(Environment.CurrentDirectory, "TestSite");
                siteDeployer.Deploy(configTransform, deployPath);
                webHostStartInfo = InitializeIisExpress(_application, deployPath);
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

        private static ProcessStartInfo InitializeIisExpress(WebApplication application, string deployPath = null)
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
                Arguments = String.Format("/path:\"{0}\" /port:{1}", deployPath ?? application.Location.FullPath, application.PortNumber),
                FileName = string.Format("{0}\\IIS Express\\iisexpress.exe", programfiles)
            };

            foreach (var variable in application.EnvironmentVariables)
                startInfo.EnvironmentVariables.Add(variable.Key, variable.Value);

            return startInfo;
        }
    }
}
