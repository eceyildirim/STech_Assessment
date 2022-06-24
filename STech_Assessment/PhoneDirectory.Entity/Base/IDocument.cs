using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.Entity.Base
{
    public interface IDocument 
    {
        string UUID { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }

        DateTime? DeletedAt { get; set; }
    }
}
