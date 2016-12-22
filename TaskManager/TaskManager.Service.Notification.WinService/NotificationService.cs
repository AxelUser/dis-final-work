using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Utils;
using TaskManager.Service.Notification.Handler;

namespace TaskManager.Service.Notification.WinService
{
    public partial class NotificationService : ServiceBase
    {
        private string smtpLogin;
        private string smtpPassword;
        private int servicePort;
        private NotificationServiceServer server;
        public NotificationService()
        {
            InitializeComponent();
            smtpLogin = ConfigurationUtils.GetSetting("SMTPLogin");
            smtpPassword = ConfigurationUtils.GetSetting("SMTPPassword");
            bool isPortParsed = int.TryParse(ConfigurationUtils.GetSetting("Port"), out servicePort);
            if (!string.IsNullOrEmpty(smtpLogin) && !string.IsNullOrEmpty(smtpPassword) && isPortParsed)
            {
                server = new NotificationServiceServer(servicePort, smtpLogin, smtpPassword);
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
            Console.WriteLine(nameof(NotificationService));
            OnStart(null);
            Console.ReadKey();
            OnStop();
        }
    }
}
