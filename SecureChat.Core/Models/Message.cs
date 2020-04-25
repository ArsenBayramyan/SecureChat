using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core.Models
{
    public class Message : IMessage
    {
        public int MessageID { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
