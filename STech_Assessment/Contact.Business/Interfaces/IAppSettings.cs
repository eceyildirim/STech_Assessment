using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Interfaces
{
    public interface IAppSettings
    {
        string Secret { get; set; }
        int MaxFailLoginCount { get; set; }
        public string ApplicationUrl { get; set; }
        public string RootUploadPath { get; set; }
        public string AppName { get; set; }
    }
}
