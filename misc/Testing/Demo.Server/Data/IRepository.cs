using Demo.Server.Models;

namespace Demo.Server.Data
{
    public interface IRepository<T> where T : class, IIdentifiable
    {
        Task AddAsync(T item);

        Task<T?> GetAsync(string id);

        Task DeleteAsync(string id);
    }
}