using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly MC_ContextDb db;

        public ServiceRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(Service services)
        {
            await db.Service.AddAsync(services);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            Service service = await db.Service.FindAsync(id);
            if (service == null)
            {
                return false;
            }

            service.IsDelete = !service.IsDelete;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Service> GetRecord(int id)
        {
            return await db.Service.FindAsync(id);
        }

        public async Task Update(Service service)
        {
            db.Entry(service).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<Service> Services => db.Service;
    }
}
