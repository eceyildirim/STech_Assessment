using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Core
{
    public enum ContactInformationType
    {
        NotSelect = 0,
        Phone = 1,
        Email = 2,
        Location = 3
    }

    public enum ReportStatus
    {
        Prepare = 10,
        Complete = -10
    }
}
