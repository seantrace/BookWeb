﻿using BookWeb.Application.Interfaces.Repositories;
using BookWeb.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IRepositoryAsync<Product> _repository;

        public ProductRepository(IRepositoryAsync<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsBrandUsed(int brandId)
        {
            var exists = await _repository.Entities.Where(b => b.BrandId == brandId).AnyAsync();
            return exists;
        }
    }
}