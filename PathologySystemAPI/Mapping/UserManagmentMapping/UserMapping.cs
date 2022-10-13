using AutoMapper;
using Infra.Domain;
using HarougeAPI.Models.UserManagmentModel.UserModel;
using System;

namespace HarougeAPI.Mapping.UserManagmentMapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<InsertUserModel, Users>().
              ForMember(des => des.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString())).
              ForMember(des => des.IsActive, opt => opt.MapFrom(src => true)).
              ForMember(des => des.CreateAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateUserModel, Users>().
              ForMember(des => des.ModifyAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
