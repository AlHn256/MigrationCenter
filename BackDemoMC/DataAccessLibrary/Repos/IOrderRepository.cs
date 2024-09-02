using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IOrderRepository
    {
        //IEnumerable GetOrderList();

        Task<Order> GetRecord(int id);

        Task Create(Order order);

        Task Update(Order order);

        Task Delete(int id);

        IQueryable<Order> Orders { get; }
    }
}
