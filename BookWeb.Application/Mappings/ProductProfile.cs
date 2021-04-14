using AutoMapper;
using BookWeb.Application.Features.Products.Commands.AddEdit;
using BookWeb.Domain.Entities.Catalog;

namespace BookWeb.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
        }
    }
}