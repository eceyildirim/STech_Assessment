using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Report.Business.Models;
using Shared.Models;

namespace Report.API
{
    public class ReportConsumer : IConsumer<SharedReport>
    {
        public async Task Consume(ConsumeContext<SharedReport> context)
        {

            var msg = context.Message;

            

        }

    }
}
