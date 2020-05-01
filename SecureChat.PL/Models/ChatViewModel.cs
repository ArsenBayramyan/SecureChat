using SecureChat.Core.Interfaces;
using System.Collections.Generic;

namespace SecureChat.PL.Models
{
    public class ChatViewModel
    {
        public  List<User> Users { get; set; }
        public IEnumerable<IMessage> Messages { get; set; }
        public User CurrentUser { get; set; }
        public IUser ToUser { get;set; }
        public string MessageBody { get; set; }
    }
}
