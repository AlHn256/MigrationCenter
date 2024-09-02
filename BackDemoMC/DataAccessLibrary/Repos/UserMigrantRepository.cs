namespace DataAccessLibrary.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccessLibrary.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class UserMigrantRepository : IUserMigrantRepository
    {
        private readonly MC_ContextDb db;

        public UserMigrantRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(UserMigrant usermigrant)
        {
            db.UserMigrant.Add(usermigrant);
            await db.SaveChangesAsync();
        }

        public async Task Delete(UserMigrant usermigrant)
        {
            if (usermigrant != null)
            {
                db.UserMigrant.Remove(usermigrant);
                await db.SaveChangesAsync();
            }
        }

        public async Task<UserMigrant> GetRecord(int id)
        {
            return await db.UserMigrant.FindAsync(id);
        }

        public async Task Update(UserMigrant usermigrant)
        {
            db.Entry(usermigrant).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<UserMigrant> UserMigrantes => db.UserMigrant;
    }
}
