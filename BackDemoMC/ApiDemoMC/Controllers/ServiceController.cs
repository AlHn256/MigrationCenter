// <copyright file="ServiceController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApiDemoMC.DTO;
    using AutoMapper;
    using DataAccessLibrary;
    using DataAccessLibrary.DataAccess;
    using DataAccessLibrary.Repos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("Service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceDb;
        private readonly IMapper mapper;

        public ServiceController(IMapper mapper, MC_ContextDb dbContext)
        {
            this.mapper = mapper;
            serviceDb = new ServiceRepository(dbContext);
        }

        [HttpGet]
        public IEnumerable<Service> Get()
        {
            List<Service> serviceList = serviceDb.Services.Where(s => s.IsDelete == false).ToList();
            return serviceList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(int id)
        {
            Service service = await serviceDb.GetRecord(id);
            if (service == null)
            {
                new NotFoundObjectResult("Сервис " + id + " не найден");
            }

            return service;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ServiceDTO serviceDTO)
        {
            Service service = await serviceDb.GetRecord(id);
            if (service == null)
            {
                new NotFoundResult();
            }

            service = mapper.Map<Service>(serviceDTO);
            await serviceDb.Update(service);
            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ServiceDTO serviceDTO)
        {
            Service service = mapper.Map<Service>(serviceDTO);
            await serviceDb.Update(service);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await serviceDb.Delete(id))
            {
                new OkObjectResult("Сервис удален.");
            }

            return new NotFoundResult();
        }
    }
}