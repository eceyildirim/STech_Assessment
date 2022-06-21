using PhoneDirectory.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.API.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string ApplicationUrl { get; set; }
        public string AppName { get; set; }
    }
}
