using Management.Persistence.Model;

namespace Management.Persistence.Repositories
{
    public interface IUserRepositoryTest : IBaseRepository<TestUser>
    {
    }
    
    public class UserRepositoryTest : BaseRepository<TestUser>, IUserRepositoryTest
    {
        public UserRepositoryTest(IConnectionString connectionString) : base(connectionString)
        {
        }
    }
}