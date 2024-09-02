using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly MC_ContextDb db;

        public UserRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(User users)
        {
            await db.Users.AddAsync(users);
            await db.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            user.IsDelete = !user.IsDelete;
            await db.SaveChangesAsync();
        }

        public async Task<User> GetUserRecord(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<User> Users => db.Users;

        public async Task Create(Agent agent)
        {
            await db.Agent.AddAsync(agent);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Agent agent)
        {
            agent.IsDelete = !agent.IsDelete;
            await db.SaveChangesAsync();
        }

        public async Task<Agent> GetAgentRecord(int id)
        {
            return await db.Agent.FindAsync(id);
        }

        public async Task Update(Agent agent)
        {
            db.Entry(agent).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<Agent> Agents => db.Agent;

        public async Task Create(Buhgalter buhgalter)
        {
            await db.Buhgalter.AddAsync(buhgalter);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Buhgalter buhgalter)
        {
            buhgalter.IsDelete = !buhgalter.IsDelete;
            await db.SaveChangesAsync();
        }

        public async Task<Buhgalter> GetBuhgalterRecord(int id)
        {
            return await db.Buhgalter.FindAsync(id);
        }

        public async Task Update(Buhgalter buhgalter)
        {
            db.Entry(buhgalter).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<Buhgalter> Buhgalters => db.Buhgalter;

        public async Task<UrAgent> GetUrAgentsRecord(int id)
        {
            return await db.UrAgent.FindAsync(id);
        }

        public async Task Create(UrAgent urAgent)
        {
            await db.UrAgent.AddAsync(urAgent);
            await db.SaveChangesAsync();
        }

        public async Task Delete(UrAgent urAgent)
        {
            urAgent.IsDelete = !urAgent.IsDelete;
            await db.SaveChangesAsync();
        }

        public async Task Update(UrAgent urAgent)
        {
            db.Entry(urAgent).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IQueryable<UrAgent> UrAgents => db.UrAgent;
    }
}
