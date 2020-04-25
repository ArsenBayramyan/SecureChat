using SecureChat.Core.Interfaces;
using System.Collections.Generic;

namespace SecureChat.PL.Models
{
    public class ChatViewModel
    {
        public List<User> Users { get; set; }
        public List<IMessage> Messages { get; set; }
    }
}
