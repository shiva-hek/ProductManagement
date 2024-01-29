using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductManagement.Application.Accounts.Commands.Register;
using ProductManagement.Domain.Aggregates.Accounts;

namespace ProductManagement.Application.Mapping
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommandRequest, User>();
            CreateMap<IdentityResult, RegisterCommandResponse>();
            CreateMap<IdentityError, IdentityErrorDto>();
        }

    }
}