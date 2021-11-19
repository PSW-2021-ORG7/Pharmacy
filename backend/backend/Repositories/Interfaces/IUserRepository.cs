using backend.Model;

namespace backend.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}
