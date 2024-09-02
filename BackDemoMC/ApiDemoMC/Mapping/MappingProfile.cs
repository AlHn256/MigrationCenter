namespace ApiDemoMC.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ApiDemoMC.Crypto;
    using ApiDemoMC.DTO;
    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using DataAccessLibrary;
    using DataAccessLibrary.Enum;
    using DataAccessLibrary.Model;
    using DataAccessLibrary.Repos;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public class MappingProfile : Profile
    {
        [AutoMap(typeof(Order))]
        public class OrderDto

        // destination members
        {
            [Ignore]
            public decimal Total { get; set; }
        }

        public MappingProfile()
        {
            CreateMap<LoginPassDTO, Admin>()
                .ForMember(x => x.Mail, s => s.MapFrom(x => x.Email))
                .ForMember(x => x.Pass, s => s.MapFrom(x => x.Password))
                .ReverseMap();

            CreateMap<RegistrationDTO, UrAgent>()
                .ForMember(x => x.Pass, s => s.MapFrom(x => Md5.Convert(x.Password)))
                .ReverseMap();

            CreateMap<AgentRegistrationDTO, Agent>()
                .ForMember(x => x.Pass, s => s.MapFrom(x => Md5.Convert(x.Password)))
                .ReverseMap();

            CreateMap<User, MailRoleDTO>()
                .ForMember(x => x.Role, s => s.MapFrom(x => (EnumRole)x.RoleId))
                .ReverseMap();

            CreateMap<MigrantDTO, Migrant>()
                .ReverseMap();

            CreateMap<Migrant, Migrant>()
                .ForMember(dto => dto.UserMigrantes, opt => opt.Ignore())
                .ForMember(dto => dto.Id, opt => opt.Ignore());

            CreateMap<OrderDTO, Order>().ReverseMap();

            CreateMap<ServiceDTO, Service>()
                .ReverseMap();

            CreateMap<Order, SimpleOrderDTO>()
                .ForMember(d => d.Date, s => s.MapFrom(x => x.Date))
                .ReverseMap();

            CreateMap<User, SimpleUserDTO>()
                .ReverseMap();

            CreateMap<UrAgent, SimpleJurpersonDTO>()
                .ReverseMap();

            CreateMap<SimplPayListDTO, PayList>()
                .ReverseMap();

            CreateMap<BuhgalterRegistrationDTO, Buhgalter>()
                .ForMember(d => d.Pass, s => s.MapFrom(x => Md5.Convert(x.Password)))
                .ReverseMap();
        }
    }
}
