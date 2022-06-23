using PhoneDirectory.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Interfaces
{
    public interface IQueueService
    {
        void CreateQueue(string channelName);

        void SendToQueue(QueueMessage message, string channelName);


    }
}
