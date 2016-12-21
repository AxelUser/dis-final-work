using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace TaskManager.ServiceBus.Handler
{
    public class ServiceBusServer
    {
        private IConnection connection;
        readonly string qNotify;
        readonly string qReport;

        public ServiceBusServer(string qNotify, string qReport, string username, string password,
            string vitualhost, string hostname, int port)
        {
            this.qNotify = qNotify;
            this.qReport = qReport;
            var factory = new ConnectionFactory()
            {
                UserName = username,
                Password = password,
                VirtualHost = vitualhost,
                HostName = hostname,
                Port = port
            };
            connection = factory.CreateConnection();
        }

        public Start()
        {

        }

        private static string GetSetting(string key, string defValue = null)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch
            {
                return defValue;
            }
        }
    }
}
