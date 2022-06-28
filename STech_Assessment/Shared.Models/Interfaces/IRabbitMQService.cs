using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Interfaces
{
    public interface IRabbitMQService
    {
        //Send message
        void SendMessages(SharedReport sharedReport);

        //Receive message
        //Task<SharedReport> ReceiveMessages(SharedReport sharedReport);
    }
}
