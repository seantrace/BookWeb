using AutoMapper;
using BookWeb.Application.Features.Documents.Commands.AddEdit;
using BookWeb.Domain.Entities;

namespace BookWeb.Application.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<AddEditDocumentCommand, Document>().ReverseMap();
        }
    }
}