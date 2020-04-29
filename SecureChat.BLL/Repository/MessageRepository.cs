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
            this._context.Find<Message>(entity).IsDeleted=true;
            this._context.SaveChanges();
            return false;
        }
        public bool DeleteById(string id)
        {
            GetByID(id).IsDeleted = true;
            this._context.SaveChanges();
            return false;
        }
        public Message GetByID(string id)
        {
            return this._context.Find<Message>(int.Parse(id));
        }
        public IEnumerable<Message> List()
        {
            var x = this._context.Messages;
            return x;
        }
        public bool Save(Message entity)
        {
            if (entity.Body==null)
            {
                return false;
            }
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
