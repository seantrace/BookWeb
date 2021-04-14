using AutoMapper;
using BookWeb.Application.Models.Audit;
using BookWeb.Application.Responses.Audit;

namespace BookWeb.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}