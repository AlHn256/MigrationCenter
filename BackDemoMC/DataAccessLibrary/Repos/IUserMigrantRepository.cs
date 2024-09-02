using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IUserMigrantRepository
    {
        Task<UserMigrant> GetRecord(int id);

        Task Create(UserMigrant usermigrant);

        Task Update(UserMigrant usermigrant);

        Task Delete(UserMigrant usermigrant);

        IQueryable<UserMigrant> UserMigrantes { get; }
    }
}
