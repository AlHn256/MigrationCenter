using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos
{
    public class MigrantRepository : IMigrantRepository
    {
        private readonly MC_ContextDb db;

        public MigrantRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(Migrant migrant)
        {
            await db.Migrant.AddAsync(migrant);
            await db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            Migrant migrant = await db.Migrant.FindAsync(id);
            if (migrant == null)
            {
                return false;
            }

            migrant.IsDelite = !migrant.IsDelite;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Migrant> GetRecord(int id)
        {
            return await db.Migrant.FindAsync(id);
        }

        public async Task<bool> Update(Migrant migrantNew)
        {
            Migrant migrant = await db.Migrant.FindAsync(migrantNew.Id);
            if (migrant == null)
            {
                return false;
            }

            migrant.Name = migrantNew.Name;
            migrant.Surname = migrantNew.Surname;
            migrant.Patronymic = migrant.Patronymic;
            migrant.PassNumber = migrantNew.PassNumber;
            migrant.PassSerri = migrantNew.PassSerri;
            db.Attach(migrant);
            await db.SaveChangesAsync();
            return true;
        }

        public IQueryable<Migrant> Migrantes => db.Migrant;
    }
}