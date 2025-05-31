using System.Linq.Expressions;

namespace Common.Domain.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(long id, CancellationToken? cancellationToken);

        Task<T?> GetTracking(long id , CancellationToken? cancellationToken);

        Task AddAsync(T entity,CancellationToken? cancellationToken);
        void Add(T entity,CancellationToken? cancellationToken);

        Task AddRange(ICollection<T> entities,CancellationToken? cancellationToken);

        void Update(T entity,CancellationToken? cancellationToken);

        Task<int> SaveChangeAsync(CancellationToken? cancellationToken);
        
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression,CancellationToken? cancellationToken);

        bool Exists(Expression<Func<T, bool>> expression,CancellationToken? cancellationToken);

        T? Get(long id,CancellationToken? cancellationToken);
    }
}