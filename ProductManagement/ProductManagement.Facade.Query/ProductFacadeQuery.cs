using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Facade.Contracts.Products.DTOs;
using ProductManagement.Facade.Contracts.Products.Services;
using ProductManagement.Facade.Query.Mappers;
using ProductManagement.Persistence;

namespace ProductManagement.Facade.Query
{
    public class ProductFacadeQuery : IProductFacadeQuery
    {
        private readonly ProductContext _context;

        public ProductFacadeQuery(ProductContext context)
        {
            _context = context;
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            return ProductMapper.Map(product);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return ProductMapper.Map(products);
        }
    }
}
