using E_Commerce.Core.Entities;

namespace E_Commerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        Task<TResult?> GetEntityWithSpec<TResult>(ISpecifacation<T, TResult> spec);
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecifacation<T, TResult> spec);



        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
