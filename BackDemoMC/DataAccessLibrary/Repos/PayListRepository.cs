using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos
{
    public class PayListRepository : IPayListRepository
    {
        private readonly MC_ContextDb db;

        public PayListRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(PayList paylist)
        {
            await db.PayList.AddAsync(paylist);
            await db.SaveChangesAsync();
        }

        public async Task<PayList> GetRecord(int id)
        {
            return await db.PayList.FindAsync(id);
        }

        public async Task Update(PayList paylist)
        {
            db.Entry(paylist).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<PayList> PayListes => db.PayList;
    }
}
