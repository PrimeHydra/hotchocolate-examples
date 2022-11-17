using Demo.Server.Models;

namespace Demo.Server.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IIdentifiable
    {
        private readonly List<T> items = new List<T>();

        public InMemoryRepository()
        {
        }

        public InMemoryRepository(IEnumerable<T> initialItems)
        {
            this.items.AddRange(initialItems);
        }

        public Task AddAsync(T item)
        {
            this.items?.Add(item);
            return Task.CompletedTask;
        }

        public Task<T?> GetAsync(string id)
        {
            return Task.FromResult(this.items.FirstOrDefault(item => item.Id == id));
        }

        public Task DeleteAsync(string id)
        {
            int index = this.items.FindIndex(item => item.Id == id);
            if (index > -1)
            {
                this.items.RemoveAt(index);
            }

            return Task.CompletedTask;
        }
    }
}