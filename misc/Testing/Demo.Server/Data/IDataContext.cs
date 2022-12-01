using Demo.Server.Models;

namespace Demo.Server.Data;

public interface IDataContext
{
    IRepository<Puppy> PuppyRepository { get; }
}