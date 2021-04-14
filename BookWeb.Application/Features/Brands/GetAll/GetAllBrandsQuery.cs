using AutoMapper;
using BookWeb.Application.Interfaces.Repositories;
using BookWeb.Domain.Entities.Catalog;
using BookWeb.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookWeb.Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQuery : IRequest<Result<List<GetAllBrandsResponse>>>
    {
        public GetAllBrandsQuery()
        {
        }
    }

    public class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<List<GetAllBrandsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBrandsCachedQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllBrandsResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _unitOfWork.Repository<Brand>().GetAllAsync();
            var mappedBrands = _mapper.Map<List<GetAllBrandsResponse>>(brandList);
            return Result<List<GetAllBrandsResponse>>.Success(mappedBrands);
        }
    }
}