using Newtonsoft.Json;
using PhoneDirectory.Business.Interfaces;
using PhoneDirectory.Business.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Events;
using System.Threading;

namespace PhoneDirectory.Business.Services
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

        public void CreateQueue(string channelName)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: channelName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);

                    Console.WriteLine(" [x] Done");

                    var queueMessage = JsonConvert.DeserializeObject<QueueMessage>(message);
                    var byteArray = Encoding.ASCII.GetBytes(message);
                    var response = _requestService.SendPostRequest(queueMessage.To, JsonConvert.SerializeObject(queueMessage));

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false); //Acknowledge message and remove from queue
                    Thread.Sleep(1000);
                };
                channel.BasicConsume(queue: channelName,
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public void SendToQueue(QueueMessage message, string channelName)
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

                string output = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(output);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: channelName,
                                     basicProperties: properties,
                                     body: body);
            }
        }
    }
}
