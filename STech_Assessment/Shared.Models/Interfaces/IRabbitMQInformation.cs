using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public interface IRabbitMQInformation
    {
        public static string RootUri { get; set; }
        public static string UserName { get; set; }
        public static string Password { get; set; }
    }
}
