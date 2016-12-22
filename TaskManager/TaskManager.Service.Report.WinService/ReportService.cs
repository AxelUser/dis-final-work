using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Service.Notification.Handler;
using TaskManager.Utils;

namespace TaskManager.Service.Report.WinService
{
    public partial class ReportService : ServiceBase
    {
        private string smtpLogin;
        private string smtpPassword;
        private int servicePort;
        private ReportServiceServer server;
        public ReportService()
        {
            InitializeComponent();
            smtpLogin = ConfigurationUtils.GetSetting("SMTPLogin");
            smtpPassword = ConfigurationUtils.GetSetting("SMTPPassword");
            bool isPortParsed = int.TryParse(ConfigurationUtils.GetSetting("Port"), out servicePort);
            if (!string.IsNullOrEmpty(smtpLogin) && !string.IsNullOrEmpty(smtpPassword) && isPortParsed)
            {
                server = new ReportServiceServer(servicePort, smtpLogin, smtpPassword);
            }
        }

        protected override void OnStart(string[] args)
        {
            server.Start();
        }

        protected override void OnStop()
        {
            server.Stop();
        }

        public void TestService()
        {
            Console.WriteLine(nameof(ReportService));
            OnStart(null);
            Console.ReadKey();
            OnStop();
        }
    }

}


