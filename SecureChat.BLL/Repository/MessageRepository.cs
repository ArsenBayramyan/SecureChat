using SecureChat.Core.Interfaces;
using SecureChat.DAL.Contexts;
using SecureChat.DAL.Models;
using System.Collections.Generic;

namespace SecureChat.BLL.Repository
{
    public class MessageRepository : IRepository<Message>
    {
        private MessagesDBContext _context;

        public MessageRepository(MessagesDBContext context)
        {
            this._context = context;
        }
        public bool Delete(Message entity)
        {
            this._context.Messages.Remove(entity);
            this._context.SaveChanges();
            return false;
        }
        public bool DeleteById(int id)
        {
            Message message = this._context.Find<Message>(id);
            this._context.Messages.Remove(message);
            return false;
        }
        public Message GetByID(int id)
        {
            return this._context.Find<Message>(id);
        }
        public IEnumerable<Message> List()
        {
            return this._context.Messages;
        }
        public bool Save(Message entity)
        {
            this._context.Messages.Add(entity);
            _context.SaveChanges();
            return true;
        }
        public bool Update(Message entity)
        {
            this._context.Update(entity);
            return true;
        }
    }
}
