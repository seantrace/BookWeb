using AutoMapper;
using BookWeb.Application.Interfaces.Repositories;
using BookWeb.Domain.Entities.Catalog;
using BookWeb.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookWeb.Application.Features.Brands.Queries.GetById
{
    public class GetBrandByIdQuery : IRequest<Result<GetBrandByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdResponse>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<GetBrandByIdResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _unitOfWork.Repository<Brand>().GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetBrandByIdResponse>(product);
                return Result<GetBrandByIdResponse>.Success(mappedProduct);
            }
        }
    }
}