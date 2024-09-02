using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiDemoMC.DTO;
using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoMC.Controllers
{
    [Route("Pay")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userDb;
        private readonly IPayListRepository paylistDb;

        public PayController(IMapper mapper, MC_ContextDb dbContext)
        {
            paylistDb = new PayListRepository(dbContext);
            this.mapper = mapper;
            userDb = new UserRepository(dbContext);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Agent,Buhgalter")]
        public ActionResult<decimal> GetAccount(int recipientId)
        {
            return GetTotalSumm(recipientId);
        }

        [HttpPost]
        [Authorize(Roles = "Agent,Buhgalter")]
        public async Task<ActionResult> Post([FromBody] SimplPayListDTO value)
        {
            value.PayerId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            PayList payList = mapper.Map<PayList>(value);
            if (value.Amount > 0)
            {
                payList.IsDebet = true;
            }
            else
            {
                if (value.Amount + GetTotalSumm(value.RecipientId) < 0)
                {
                    return new BadRequestObjectResult("На счету не достаточно средств!");
                }
            }

            payList.Date = DateTime.Now;

            await paylistDb.Create(payList);
            return new OkObjectResult(payList);
        }

        private decimal GetTotalSumm(int recipientId)
        {
            decimal totalSumm = paylistDb.PayListes
                .Where(p => p.IsDelete == false && p.RecipientId == recipientId)
                    .ToList().Select(a => a.Amount)
                    .Sum();

            return totalSumm;
        }
    }
}