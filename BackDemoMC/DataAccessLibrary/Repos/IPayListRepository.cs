using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repos
{
    public interface IPayListRepository
    {
        IQueryable<PayList> PayListes { get; }

        Task<PayList> GetRecord(int id);

        Task Create(PayList paylist);

        Task Update(PayList paylist);
    }
}
