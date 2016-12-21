using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Configuration;

namespace TaskManager.ServiceBus.Handler
{
    public class ServiceBusServer
    {
        private IConnection connection;
        readonly string qNotify;
        readonly string qReport;
        private QueueListener listenerNotify;
        private QueueListener listenerReport;

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
            listenerNotify = null;
            listenerReport = null;
        }

        public void InitNotifyService(string dictKey, string hostname, int port)
        {
            listenerNotify = CreateListener(connection, qNotify, dictKey, hostname, port);
        }

        public void InitReportService(string dictKey, string hostname, int port)
        {
            listenerReport = CreateListener(connection, qReport, dictKey, hostname, port);
        }

        public void Start()
        {
            listenerNotify?.Start();
            //listenerReport?.Start();
        }

        public void Stop()
        {
            listenerNotify?.Stop();
            //listenerReport?.Stop();
        }

        private QueueListener CreateListener(IConnection connetction, string queueName, string dictKey, string serviceHost, int servicePort)
        {
            ServiceSender sender = new ServiceSender(dictKey, serviceHost, servicePort);
            return new QueueListener(connection, queueName, sender);
        }
    }
}
