using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IUserRepository
    {
        // UserRepository
        IQueryable<User> Users { get; }

        Task<User> GetUserRecord(int id);

        Task Create(User users);

        Task Update(User users);

        Task Delete(User users);

        // AgentsRepository
        IQueryable<Agent> Agents { get; }

        Task<Agent> GetAgentRecord(int id);

        Task Create(Agent agent);

        Task Update(Agent agent);

        Task Delete(Agent agent);

        // UrAgentsRepository
        IQueryable<UrAgent> UrAgents { get; }

        Task<UrAgent> GetUrAgentsRecord(int id);

        Task Create(UrAgent urAgent);

        Task Update(UrAgent urAgent);

        Task Delete(UrAgent urAgent);

        // BuhgalterRepository
        IQueryable<Buhgalter> Buhgalters { get; }

        Task<Buhgalter> GetBuhgalterRecord(int id);

        Task Create(Buhgalter buhgalter);

        Task Update(Buhgalter buhgalter);

        Task Delete(Buhgalter buhgalter);
    }
}
