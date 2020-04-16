using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core.Interfaces
{
    public interface IMessage
    {
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }
        DateTime SendDate { get; set; }
    }
}
