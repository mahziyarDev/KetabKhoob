using System.Linq.Expressions;

namespace Common.Domain.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(long id, CancellationToken? cancellationToken);

        Task<T?> GetTrackingAsync(long id , CancellationToken? cancellationToken);
        T? GetTracking(long id);

        Task AddAsync(T entity,CancellationToken? cancellationToken);
        void Add(T entity);

        Task AddRange(ICollection<T> entities,CancellationToken? cancellationToken);

        void Update(T entity);

        Task<int> SaveChangeAsync(CancellationToken? cancellationToken);
        int SaveChange();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression,CancellationToken? cancellationToken);

        bool Exists(Expression<Func<T, bool>> expression);

        T? Get(long id);
    }
}