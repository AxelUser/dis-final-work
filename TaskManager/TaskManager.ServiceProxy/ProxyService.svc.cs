using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RabbitMQ.Client;
using System.Configuration;
using TaskManager.Utils;

namespace TaskManager.ServiceProxy
{
    public class ProxyService : IProxyService
    {
        private IConnection connection;
        readonly string qNotify;
        readonly string qReport;
        public ProxyService()
        {
            qNotify = ConfigurationUtils.GetSetting("QueueNotifyName", "q_notify");
            qReport = ConfigurationUtils.GetSetting("QueueReportName", "q_report");
            string username = ConfigurationUtils.GetSetting("MQUserName", "guest");
            string password = ConfigurationUtils.GetSetting("MQPassword", "guest");
            string vitualhost = ConfigurationUtils.GetSetting("MQVirtualHost", "/");
            string hostname = ConfigurationUtils.GetSetting("MQHostName", "localhost");
            string port = ConfigurationUtils.GetSetting("MQPort", "5672");
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
                props.Headers = new Dictionary<string, object>();
                props.Headers.Add("task-id", taskId);
                
                channel.QueueDeclare(
                    queue: qNotify,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
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
                props.Headers = new Dictionary<string, object>();
                props.Headers.Add("project-id", projectId);                
                channel.QueueDeclare(
                    queue: qReport,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: qReport,
                    basicProperties: props);
            }
        }
    }
}
