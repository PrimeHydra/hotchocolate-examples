using Demo.Server.Models;

namespace Demo.Server.Data
{
    public class DataContext : IDataContext
    {
        public IRepository<Puppy> PuppyRepository { get; }

        public DataContext(IRepository<Puppy> puppyRepository)
        {
            this.PuppyRepository = puppyRepository;
        }
    }
}
