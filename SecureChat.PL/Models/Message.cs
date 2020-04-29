using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureChat.PL.Models
{
    public class Message:IMessage
    {
        public int MessageID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public DateTime? SendDate { get; set; }
        public bool? Status { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
