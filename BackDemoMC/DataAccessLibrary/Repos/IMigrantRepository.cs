using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IMigrantRepository
    {
        IQueryable<Migrant> Migrantes { get; }

        Task<Migrant> GetRecord(int id);

        Task Create(Migrant migrant);

        Task<bool> Update(Migrant migrant);

        Task<bool> Delete(int id);
    }
}
