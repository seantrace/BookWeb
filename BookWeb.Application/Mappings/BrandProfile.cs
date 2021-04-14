using AutoMapper;
using BookWeb.Application.Features.Brands.AddEdit;
using BookWeb.Application.Features.Brands.Queries.GetAll;
using BookWeb.Application.Features.Brands.Queries.GetById;
using BookWeb.Domain.Entities.Catalog;

namespace BookWeb.Application.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}