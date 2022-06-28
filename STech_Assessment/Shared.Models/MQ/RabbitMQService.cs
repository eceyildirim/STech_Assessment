using MassTransit;
using Shared.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.MQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IBus _bus;
        private readonly RabbitMQInformation RabbitMQInformation = new RabbitMQInformation();

        public RabbitMQService(IBus bus)
        {
            _bus = bus;
        }

        //public Task<SharedReport> ReceiveMessages(SharedReport sharedReport)
        //{
        //    throw new NotImplementedException();
        //}

        public async void SendMessages(SharedReport sharedReport)
        {
            Uri uri = new Uri(RabbitMQInformation.Uri);
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(sharedReport);
        }
    }
}
