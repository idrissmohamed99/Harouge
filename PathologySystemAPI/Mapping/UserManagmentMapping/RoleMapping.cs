using AutoMapper;
using Infra.Domain;
using HarougeAPI.Models.UserManagmentModel.RoleModel;
using System;

namespace HarougeAPI.Mapping.UserManagmentMapping
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<InsertRoleModel, Roles>().
               ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())).
               ForMember(des => des.IsActive, opt => opt.MapFrom(src => true)).
               ForMember(des => des.CreateAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateRoleModel, Roles>().
               ForMember(des => des.ModifyAt, opt => opt.MapFrom(src => DateTime.Now));
        }

    }
}
