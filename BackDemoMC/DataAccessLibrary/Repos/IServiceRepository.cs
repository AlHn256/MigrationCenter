using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IServiceRepository
    {
        IQueryable<Service> Services { get; }

        Task<Service> GetRecord(int id);

        Task Create(Service services);

        Task Update(Service services);

        Task<bool> Delete(int id);
    }
}