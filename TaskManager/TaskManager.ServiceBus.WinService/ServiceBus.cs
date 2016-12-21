using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TaskManager.ServiceBus.Handler;
using TaskManager.Utils;

namespace TaskManager.ServiceBus.WinService
{
    public partial class ServiceBus : ServiceBase
    {
        private ServiceBusServer server;
        public ServiceBus()
        {
            InitializeComponent();
            string qNotify = ConfigurationUtils.GetSetting("QueueNotifyName", "q_notify");
            string qReport = ConfigurationUtils.GetSetting("QueueReportName", "q_report");
            string username = ConfigurationUtils.GetSetting("MQUserName", "guest");
            string password = ConfigurationUtils.GetSetting("MQPassword", "guest");
            string vitualhost = ConfigurationUtils.GetSetting("MQVirtualHost", "/");
            string hostname = ConfigurationUtils.GetSetting("MQHostName", "localhost");
            string port = ConfigurationUtils.GetSetting("MQPort", "5672");

            server = new ServiceBusServer(
                qNotify: qNotify,
                qReport: qReport,
                username: username,
                password: password,
                vitualhost: vitualhost,
                hostname: hostname,
                port: int.Parse(port));
        }

        protected override void OnStart(string[] args)
        {
            ConnectNotifyService(ref server);
            ConnectReportService(ref server);
            server.Start();
        }

        protected override void OnStop()
        {
            server.Stop();
        }

        private bool ConnectNotifyService(ref ServiceBusServer server)
        {
            bool isConnected;
            string host = ConfigurationUtils.GetSetting("NotifyServiceHost");
            string port = ConfigurationUtils.GetSetting("NotifyServicePort");
            string key = ConfigurationUtils.GetSetting("NotifyServiceKey");
            if (isConnected = (host != null &&  port != null && key != null))
            {
                int parsedPort;
                if(isConnected &= int.TryParse(port, out parsedPort))
                {
                    server.InitNotifyService(key, host, parsedPort);
                }
            }
            return isConnected;
        }

        private bool ConnectReportService(ref ServiceBusServer server)
        {
            bool isConnected;
            string host = ConfigurationUtils.GetSetting("ReportServiceHost");
            string port = ConfigurationUtils.GetSetting("ReportServicePort");
            string key = ConfigurationUtils.GetSetting("ReportServiceKey");
            if (isConnected = (host != null && port != null && key != null))
            {
                int parsedPort;
                if (isConnected &= int.TryParse(port, out parsedPort))
                {
                    server.InitNotifyService(key, host, parsedPort);
                }
            }
            return isConnected;
        }

        public void TestSerivce()
        {
            OnStart(null);
            Console.ReadKey();
            OnStop();
        }
    }
}
