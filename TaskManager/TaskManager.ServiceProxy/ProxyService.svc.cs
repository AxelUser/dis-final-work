using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RabbitMQ.Client;
using System.Configuration;

namespace TaskManager.ServiceProxy
{
    public class ProxyService : IProxyService
    {
        private IConnection connection;
        readonly string qNotify;
        readonly string qReport;
        public ProxyService()
        {
            qNotify = GetSetting("QueueNotifyName", "q_notify");
            qReport = GetSetting("QueueReportName", "q_report");
            string username = GetSetting("MQUserName", "guest");
            string password = GetSetting("MQPassword", "guest");
            string vitualhost = GetSetting("MQVirtualHost", "/");
            string hostname = GetSetting("MQHostName", "localhost");
            string port = GetSetting("MQPort", "5672");
            var factory = new ConnectionFactory()
            {
                UserName = username,
                Password = password,
                VirtualHost = vitualhost,
                HostName = hostname,
                Port = int.Parse(port)
            };
            connection = factory.CreateConnection();
        }
        public void Notify(int taskId)
        {
            using(IModel channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.Headers.Add("task-id", taskId.ToString());
                
                channel.QueueDeclare(
                    queue: qNotify,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                byte[] body = BitConverter.GetBytes(taskId);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: qNotify,
                    basicProperties: props);
            }
        }

        public void Report(int projectId)
        {
            using (IModel channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.Headers.Add("project-id", projectId.ToString());
                channel.QueueDeclare(
                    queue: qReport,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                byte[] body = BitConverter.GetBytes(projectId);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: qReport,
                    basicProperties: null,
                    body: body);
            }
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
