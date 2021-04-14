using AutoMapper;
using BookWeb.Application.Responses.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookWeb.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, IdentityRole>().ReverseMap();
        }
    }
}