using System.Collections.Generic;

namespace SecureChat.PL.Models
{
    public class ChatViewModel
    {
        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}
