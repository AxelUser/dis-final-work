using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RabbitMQ.Client;

namespace TaskManager.ServiceProxy
{
    public class ProxyService : IProxyService
    {
        private IConnection connection;
        readonly string qNotify = "q_notify";
        readonly string qReport = "q_report";
        public ProxyService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
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
                props.Headers.Add("task-id", projectId.ToString());
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
    }
}
