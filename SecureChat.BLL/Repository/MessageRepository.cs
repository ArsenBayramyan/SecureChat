using SecureChat.Core.Interfaces;
using SecureChat.DAL.Contexts;
using System;
using System.Collections.Generic;

namespace SecureChat.BLL.Repository
{
    class MessageRepository : IRepository<IMessage>
    {
        MessagesDBContext _context;

        public MessageRepository(MessagesDBContext context)
        {
            this._context = context;
        }

        public bool Delete(IMessage entity)
        {
            this._context.Messages.Remove(entity);
            this._context.SaveChanges();
            return false;
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IMessage GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessage> List()
        {
            throw new NotImplementedException();
        }

        public bool Save(IMessage entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(IMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
