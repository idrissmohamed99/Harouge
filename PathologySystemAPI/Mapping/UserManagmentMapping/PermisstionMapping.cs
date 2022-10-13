using AutoMapper;
using Infra.Domain;
using HarougeAPI.Models.UserManagmentModel.PermisstionModel;
using System;

namespace HarougeAPI.Mapping
{
    public class PermisstionMapping : Profile
    {
        public PermisstionMapping()
        {
            CreateMap<InsertPermisstionModel, Permisstions>().
                ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())).
                ForMember(des => des.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdatePermisstionModel, Permisstions>();
        }
    }
}
