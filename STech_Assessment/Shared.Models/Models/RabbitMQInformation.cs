using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public class RabbitMQInformation : IRabbitMQInformation
    {
        public static string Uri { get; set; } = "rabbitmq://localhost/reportQueue";
        public static string UserName { get; set; } = "guest";
        public static string Password { get; set; } = "guest";
        public static string RootUri { get; set; } = "rabbitmq://localhost/";
    }
}
