using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TaskManager.ServiceBus.Handler
{
    public class QueueListener
    {
        private EventingBasicConsumer mqConsumer;
        private IModel model;
        private string queueName;

        public QueueListener(IConnection connection, string queueName, ServiceSender sender)
        {
            model = connection.CreateModel();
            mqConsumer = new EventingBasicConsumer(model);
            mqConsumer.Received += (s, ea) =>
            {
                bool isSuccess = sender.Send(ea.BasicProperties.Headers);
                if (isSuccess)
                {
                    model.BasicAck(ea.DeliveryTag, false);
                }
            };
            this.queueName = queueName;
        }

        public void Start()
        {
            model.BasicConsume(queueName, false, mqConsumer);
        }
        
        public void Stop()
        {
            model.BasicCancel(mqConsumer.ConsumerTag);
        }
    }
}
