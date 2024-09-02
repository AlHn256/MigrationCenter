namespace ApiDemoMC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ApiDemoMC.DTO;
    using AutoMapper;
    using DataAccessLibrary;
    using DataAccessLibrary.DataAccess;
    using DataAccessLibrary.Repos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("Migrant")]
    [ApiController]
    public class MigrantController : ControllerBase
    {
        private IMigrantRepository migrantDb;
        private IUserRepository userDb;
        private IUserMigrantRepository userMigrantDB;
        private readonly IMapper mapper;

        public MigrantController(IMapper mapper, MC_ContextDb dbContext)
        {
            this.mapper = mapper;
            userDb = new UserRepository(dbContext);
            migrantDb = new MigrantRepository(dbContext);
            userMigrantDB = new UserMigrantRepository(dbContext);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] MigrantDTO value)
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            User user = await userDb.GetUserRecord(id);

            Migrant migrant = mapper.Map<Migrant>(value);

            await migrantDb.Create(migrant);
            UserMigrant usermigrant = new UserMigrant
            {
                UserId = user.Id,
                MigrantId = migrant.Id,
            };

            await userMigrantDB.Create(usermigrant);
            return new OkResult();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Migrant>> Get()
        {
            List<Migrant> migrantsList = migrantDb.Migrantes.Where(m => m.Id > 0).ToList();
            return migrantsList;
        }

        [Authorize]
        [HttpGet("GetUserMigrantList")]
        public async Task<ActionResult<IEnumerable<Migrant>>> GetUserMigrantList()
        {
            List<Migrant> migrantsList = new List<Migrant>();

            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            User user = await userDb.GetUserRecord(id);

            List<UserMigrant> listUsersMigrant = userMigrantDB.UserMigrantes.Where(p => p.UserId == user.Id).ToList();

            foreach (UserMigrant entity in listUsersMigrant)
            {
                Migrant migrant = await migrantDb.GetRecord(entity.MigrantId);
                if (migrant.IsDelite == false)
                {
                    migrant.UserMigrantes = null;
                    migrantsList.Add(migrant);
                }
            }

            return migrantsList;
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<string>> Put([FromBody] MigrantDTO migrantDto)
        {
            Migrant migrant = mapper.Map<Migrant>(migrantDto);

            if (await migrantDb.Update(migrant))
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }

        [Authorize(Roles = "Jurperson, Admin")]
        [HttpDelete("AddDelMigrant/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await migrantDb.Delete(id))
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
