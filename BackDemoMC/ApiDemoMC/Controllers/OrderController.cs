using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiDemoMC.DTO;
using AutoMapper;
using DataAccessLibrary;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Enum;
using DataAccessLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemoMC.Controllers
{
    [Route("Order")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userDb;
        private readonly IOrderRepository orderDb;
        private readonly IMigrantRepository migrantDB;
        private readonly IServiceRepository serviceDB;

        public OrderController(IMapper mapper, MC_ContextDb dbContext)
        {
            serviceDB = new ServiceRepository(dbContext);
            userDb = new UserRepository(dbContext);
            orderDb = new OrderRepository(dbContext);
            migrantDB = new MigrantRepository(dbContext);
            this.mapper = mapper;
        }

        [Authorize(Roles = "Agent,Jurperson")]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await orderDb.GetRecord(id);
            if (order == null)
            {
                return new BadRequestObjectResult("Заявка не найдена!");
            }

            List<MigrantServiceListDTO> migrantsList = new List<MigrantServiceListDTO>();
            foreach (OrderItem entity in order.OrderItems)
            {
                if (migrantsList.FirstOrDefault(m => m.MigrantId == entity.MigrantId) == null)
                {
                    List<int> servicesId = new List<int>();
                    migrantsList.Add(new MigrantServiceListDTO()
                    {
                        MigrantId = entity.MigrantId,
                        ServicesId = servicesId,
                    });
                }
            }

            foreach (MigrantServiceListDTO entity in migrantsList)
            {
                List<OrderItem> orderItemes = order.OrderItems.Where(o => o.MigrantId == entity.MigrantId).ToList();
                Migrant migrant = await migrantDB.GetRecord(entity.MigrantId);
                List<ServiceDTO> servicesListDto = new List<ServiceDTO>();

                foreach (OrderItem item in orderItemes)
                {
                    Service service = await serviceDB.GetRecord(item.ServiceId);
                    ServiceDTO serviceDTO = new ServiceDTO
                    {
                        Cost = service.Cost,
                        Name = service.Name,
                    };

                    servicesListDto.Add(serviceDTO);
                }

                entity.MigrantName = migrant.Name;
                entity.PassNumber = migrant.PassNumber;
                entity.PassSerri = migrant.PassSerri;
                entity.ServicesList = servicesListDto;
            }

            MigrantServiceListDTO migrantServiceListDTO = new MigrantServiceListDTO();

            OrderDTO orderDTO = new OrderDTO
            {
                Name = order.Name,
                Migrants = migrantsList,
            };
            return orderDTO;
        }

        // [AllowAnonymous]
        [Authorize(Roles = "Agent,Jurperson")]
        [HttpGet("OrdersByUser/{userId}")]
        public async Task<IEnumerable<SimpleOrderDTO>> GetOrdersByUser(int userId)
        {
            List<Order> ordersList = new List<Order>();
            List<SimpleOrderDTO> ordersListDto = new List<SimpleOrderDTO>();

            ordersList = orderDb.Orders.Where(p => p.UserId == userId && p.IsDelete != true).ToList();

            foreach (Order entity in ordersList)
            {
                int entitySummOfServices = 0;
                if (entity.OrderItems != null)
                {
                    foreach (OrderItem item in entity.OrderItems)
                    {
                        Service service = await serviceDB.GetRecord(item.ServiceId);
                        entitySummOfServices += service.Cost;

                         // entitySummOfServices += serviceDB.GetRecord(item.ServiceId).Cost;
                    }
                }

                var orderCostDto = new SimpleOrderDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Date = entity.Date.ToString("d"),
                    Status = entity.Status,
                    Cost = entitySummOfServices,
                };
                ordersListDto.Add(orderCostDto);
            }

            return ordersListDto;
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<IEnumerable<SimpleOrderDTO>> Get()
        {
            List<Order> ordersList = new List<Order>();
            List<SimpleOrderDTO> ordersListDto = new List<SimpleOrderDTO>();

            User user = await userDb.GetUserRecord(int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value));
            ordersList = orderDb.Orders.Where(o => o.UserId == user.Id && o.IsDelete == false).ToList();

            foreach (Order entity in ordersList)
            {
                // ??Пересчитать сумму
                // int entitySummOfServices = 0;
                // if (entity.OrderItems != null)
                // {
                //     foreach (OrderItem item in entity.OrderItems)
                //     {
                //         entitySummOfServices += serviceDB.GetRecord(item.ServiceId).Cost;
                //     }
                // }
                SimpleOrderDTO orderCostDto = mapper.Map<SimpleOrderDTO>(entity);

                ordersListDto.Add(orderCostDto);
            }

            return ordersListDto;
        }

        [Authorize]
        [HttpPost("AddOrder")]
        public ActionResult AddOrder([FromBody] OrderDTO value)
        {
            List<MigrantServiceListDTO> migrants = value.Migrants;
            if (migrants.Count == 0)
            {
                return new BadRequestObjectResult("Список мигрантов путой!");
            }

            // ??Order order = this.mapper.Map<Order>(value);
            List<OrderItem> migrantServiceList = new List<OrderItem>();

            foreach (MigrantServiceListDTO migrant in migrants)
            {
                List<int> servicesIdList = migrant.ServicesId;
                foreach (int serviceid in servicesIdList)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        MigrantId = migrant.MigrantId,
                        ServiceId = serviceid,
                    };
                    migrantServiceList.Add(orderItem);
                }
            }

            if (migrantServiceList.Count == 0)
            {
                return new BadRequestObjectResult("Список сервисов пуcтой!");
            }

            Order order = new Order
            {
                Name = "Заявка № " + (orderDb.Orders.Max(p => p.Id) + 1).ToString(),
                Status = EnumOrderStatus.New,
                Date = DateTime.Now,
                UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
                OrderItems = migrantServiceList,
            };

            // ??orderDb.Create(order);
            return new OkObjectResult("Ok");
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SimpleOrderDTO value)
        {
            Order order = await orderDb.GetRecord(value.Id);

            if (order.Status == EnumOrderStatus.IsPaid)
            {
                return new BadRequestObjectResult("Заявка уже оплачена. Менять нельзя!");
            }

            order = mapper.Map<Order>(value);
            await orderDb.Update(order);
            return new OkObjectResult(mapper.Map<OrderDTO>(order));
        }
    }
}