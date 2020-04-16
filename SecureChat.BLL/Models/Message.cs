using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.BLL.Models
{
    class Message : IMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
    }
}
