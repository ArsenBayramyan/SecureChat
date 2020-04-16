using System.Collections.Generic;

namespace SecureChat.Core.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        bool Save(T entity);
        bool Delete(T entity);
        T GetByID(int id);
        bool DeleteById(int id);
        bool Update(T entity);
    }
}
