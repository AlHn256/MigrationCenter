using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiDemoMC.Crypto;
using ApiDemoMC.DTO;
using ApiDemoMC.Model;
using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Enum;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ApiDemoMC.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userDb;
        private readonly IOrderRepository orderDb;
        private readonly IPayListRepository paylistDb;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, MC_ContextDb dbContext)
        {
            this.mapper = mapper;
            this.logger = logger;
            paylistDb = new PayListRepository(dbContext);
            userDb = new UserRepository(dbContext);
            orderDb = new OrderRepository(dbContext);
        }

        [HttpGet("GetString")]
        public string GetString()
        {
            return "Hello World";
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginPassDTO value)
        {
            logger.LogInformation("\n\n" + DateTime.Now + " Start login user " + value.Email + " \n");

            User user = userDb.Users.FirstOrDefault(u => u.Mail == value.Email && u.Pass == Md5.Convert(value.Password));
            if (user == null)
            {
                logger.LogDebug("\nLogin - Fail  user " + value.Email + " \nНеверный логин или пароль!\n");
                return new UnauthorizedObjectResult("Неверный логин или пароль!");
            }

            if (user.RoleId == (int)EnumRole.Buhgalter)
            {
                Buhgalter buhgalter = await userDb.GetBuhgalterRecord(user.Id);
                if (buhgalter.EndDateLicens < DateTime.Now)
                {
                    logger.LogDebug("\nLogin - Fail  user " + value.Email + " \nЛицензия просрочена! Срок дейсвия лицензии закончился: " + buhgalter.EndDateLicens.ToShortDateString() + "\n");
                    return new BadRequestObjectResult("Лицензия просрочена! Срок дейсвия лицензии закончился: " + buhgalter.EndDateLicens.ToShortDateString());
                }
            }

            logger.LogInformation("\nLogin Ok \n");
            string jwt = GetJwt(user.Id, user.RoleId);
            return new OkObjectResult(jwt);
        }

        [Authorize]
        [HttpGet("GetRole")]
        public async Task<MailRoleDTO> GetRole()
        {
            //User.Identity.IsAuthenticated
            User user = await userDb.GetUserRecord(int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));
            MailRoleDTO mailrole = mapper.Map<MailRoleDTO>(user);
            return mailrole;
        }

        [Authorize(Roles = "Agent,Buhgalter")]
        [HttpGet("GetJurperson")]
        public ActionResult<IEnumerable<SimpleJurpersonDTO>> Get()
        {
            var users = userDb.UrAgents.Where(u => u.RoleId == (int)EnumRole.Jurperson && u.IsDelete == false).ToList();
            var jurpersonsDTO = new List<SimpleJurpersonDTO>();
            var isBuhgalter = User.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value == EnumRole.Buhgalter.ToString();
            foreach (UrAgent entity in users)
            {
                if (isBuhgalter || orderDb.Orders.FirstOrDefault(o => o.UserId == entity.Id) != null)
                {
                    SimpleJurpersonDTO simpleJurperson = mapper.Map<SimpleJurpersonDTO>(entity);
                    simpleJurperson.Account = paylistDb.PayListes
                        .Where(p => p.IsDelete == false && p.RecipientId == entity.Id)
                            .ToList().Select(a => a.Amount)
                            .Sum();

                    jurpersonsDTO.Add(simpleJurperson);
                }
            }

            return jurpersonsDTO;
        }

        [HttpPost("AgentRegistration")]
        public async Task<ActionResult<string>> Registration([FromBody] AgentRegistrationDTO value)
        {
            if (value.Mail.Length == 0 || value.Password.Length == 0)
            {
                return new BadRequestObjectResult("Некоторые поля незаполнены!");
            }

            User user = userDb.Users.FirstOrDefault(u => u.Mail == value.Mail);
            if (user != null)
            {
                return new BadRequestObjectResult("Ползователь с такой почтой " + value.Mail + " уже зарегистрирован!!!");
            }

            Agent agent = mapper.Map<Agent>(value);
            agent.RoleId = (int)EnumRole.Agent;
            await userDb.Create(agent);
            return new OkObjectResult("Агент " + agent.Name + " зарегистрирован.");
        }

        [HttpPost("BuhgalterRegistration")]
        public async Task<ActionResult<string>> Registration([FromBody] BuhgalterRegistrationDTO value)
        {
            if (value.Mail.Length == 0 || value.Password.Length == 0)
            {
                return new BadRequestObjectResult("Некоторые поля незаполнены!");
            }

            User user = userDb.Users.FirstOrDefault(u => u.Mail == value.Mail);
            if (user != null)
            {
                return new BadRequestObjectResult("Ползователь с такой почтой " + value.Mail + " уже зарегистрирован!!!");
            }

            if (value.EndDateLicens < value.StartDateLicens || value.EndDateLicens < DateTime.Now)
            {
                return new BadRequestObjectResult("Не верно указан срок действия лицензии");
            }

            Buhgalter buhgalter = mapper.Map<Buhgalter>(value);
            buhgalter.RoleId = (int)EnumRole.Buhgalter;
            await userDb.Create(buhgalter);
            return new OkObjectResult("Бухгалтер " + buhgalter.Name + " зарегистрирован.");
        }

        [HttpPost("UrAgentRegistration")]
        public async Task<ActionResult<string>> Registration([FromBody]RegistrationDTO value)
        {
            if (value.Mail.Length == 0 || value.Password.Length == 0)
            {
                return new BadRequestObjectResult("Некоторые поля незаполнены!");
            }

            User user = userDb.Users.FirstOrDefault(u => u.Mail == value.Mail);
            if (user != null)
            {
                return new BadRequestObjectResult("Пользователь с такой почтой " + value.Mail + " уже зарегистрирован!!!");
            }

            UrAgent urAgent = mapper.Map<UrAgent>(value);
            urAgent.RoleId = (int)EnumRole.Jurperson;
            await userDb.Create(urAgent);
            string token = GetJwt(urAgent.Id, urAgent.RoleId);
            return new OkObjectResult(token);
        }

        public string GetJwt(int userId, int roleId)
        {
            var identity = this.GetClaims(userId, roleId);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetClaims(int userId, int roleId)
        {
            var claims = new List<Claim>
                  {
                      new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                      new Claim(ClaimsIdentity.DefaultRoleClaimType, ((EnumRole)roleId).ToString()),
                  };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
