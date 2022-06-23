using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Business.Interfaces;
using Report.Business.Models;
using Report.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Services
{
    public class QueueService : IQueueService
    {
        private readonly IRequestService _requestService;
        public QueueService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public void ReceiveQueue(string channelName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };            
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: channelName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);                    
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: channelName,
                                     autoAck: true,
                                     consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
