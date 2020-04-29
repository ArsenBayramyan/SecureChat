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

        public List<SecureChat.DAL.Models.Message> GetMessages(string from, string to)
        {
            var messages = repository.List().ToList();
            var messagesDecoding = new List<SecureChat.DAL.Models.Message>();
            var message = new SecureChat.DAL.Models.Message();
            foreach (var item in messages)
            {
                message.Body = item.Body.DecodingMatrix();
                messagesDecoding.Add(message);
            }
            return messagesDecoding.Where(m => (m.To == to && m.From == from) || (m.To == from && m.From == to)).ToList();
        }
        public bool Delete(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message
            {
                MessageID=message.MessageID,
                From = message.From,
                To = message.To,
                Body = message.Body,
                SendDate=message.SendDate,
                Status=message.Status,
                IsDeleted = true
            };
            return repository.Delete(messageD);
        }
        public bool SendMessage(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message {
                MessageID=message.MessageID,
                From = message.From,
                To = message.To,
                Body = message.Body.CodeingCaesar(),
                SendDate = DateTime.Now,
                Status=message.Status,
                IsDeleted=false
            };
            return repository.Save(messageD);
        }
       
       
       
        
    }
}
