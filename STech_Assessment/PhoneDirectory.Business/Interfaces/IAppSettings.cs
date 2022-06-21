using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Business.Interfaces
{
    public interface IAppSettings
    {
        public string ApplicationUrl { get; set; }
        public string AppName { get; set; }
    }
}
