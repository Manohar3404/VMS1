using System.Linq.Expressions;

namespace VMS1.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        void Add(T entity);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Remove(T  entity); 
        void RemoveRange(IEnumerable<T> entity);
    }
}
