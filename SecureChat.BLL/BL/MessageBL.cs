using SecureChat.BLL.Models;
using SecureChat.BLL.Repository;
using SecureChat.Core.Helper;
using SecureChat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecureChat.BLL.BL
{
    public class MessageBL:BaseBL<MessageRepository>
    {
        public MessageBL(MessageRepository repository) : base(repository) { }

        public List<IMessage> GetMessages(int from, int to)
        {
            List<IMessage> messages = new List<IMessage>();
            foreach (var item in repository.List().ToList())
            {
                messages.Add(item);
            };
            messages.Where(m => (m.To == to && m.From == from) || (m.To ==from && m.From == to));
            foreach (var message in messages)
            {
                message.Body = message.Body.DecodingMatrix();
            }
            return messages;
        }
        public bool Delete(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message
            {
                From = message.From,
                To = message.To,
                Body = message.Body,
                IsDeleted = message.IsDeleted
            };
            return repository.Delete(messageD);
        }
        public bool SendMessage(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message {
                From = message.From,
                To = message.To,
                Body = message.Body.CodeingCaesar(),
                SendDate = DateTime.Now };
            return repository.Save(messageD);
        }
       
       
       
        
    }
}
