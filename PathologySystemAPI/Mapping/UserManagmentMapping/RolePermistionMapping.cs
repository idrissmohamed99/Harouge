using AutoMapper;
using Infra.Domain;
using HarougeAPI.Models.UserManagmentModel;
using System;

namespace HarougeAPI.Mapping.UserManagmentMapping
{
    public class RolePermistionMapping : Profile
    {
        public RolePermistionMapping()
        {
            CreateMap<RolePermisstionModel, RolePermisstion>().
                ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));
        }
    }
}
