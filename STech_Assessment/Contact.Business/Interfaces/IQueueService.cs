using Report.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Interfaces
{
    public interface IQueueService
    {
        void ReceiveQueue(string channelName);
    }
}
