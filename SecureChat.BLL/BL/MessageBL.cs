﻿using SecureChat.BLL.Models;
using SecureChat.BLL.Repository;
using SecureChat.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureChat.BLL.BL
{
    public class MessageBL : BaseBL<MessageRepository>
    {
        public MessageBL(MessageRepository repository) : base(repository) { }

        
        public List<SecureChat.DAL.Models.Message> GetMessages(string from, string to)
        {
            
            var messagesList = repository.List().ToList();
            var messages = messagesList.Where(m => m.To == to && m.From == from && m.IsDeleted == false || m.To == from && m.From == to && m.IsDeleted == false).ToList();
            foreach (var item in messages)
            {
                item.Body = item.Body/*.DecodingWithMatrix();*/;
            }
            return messages;
        }
        public bool Delete(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message
            {
                MessageID = message.MessageID,
                From = message.From,
                To = message.To,
                Body = message.Body,
                SendDate = message.SendDate,
                Status = message.Status,
                IsDeleted = message.IsDeleted
            };
            return repository.Delete(messageD);
        }
        public bool DeleteById(string Id)
        {
            return repository.DeleteById(Id);
        }
        public bool SendMessage(Message message)
        {
            var messageD = new SecureChat.DAL.Models.Message
            {
                MessageID = message.MessageID,
                From = message.From,
                To = message.To,
                Body = message.Body/*.CodeingCaesar(),*/,
                SendDate = DateTime.Now,
                Status = message.Status,
                IsDeleted = false
            };
            return repository.Save(messageD);
        }




    }
}
