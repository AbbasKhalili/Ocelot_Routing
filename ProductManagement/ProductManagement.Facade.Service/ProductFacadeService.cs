using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;
using ProductManagement.Facade.Contracts.Products.Services;
using ProductManagement.Facade.Contracts.Products.ViewModels;
using ProductManagement.Persistence;

namespace ProductManagement.Facade.Service
{
    public class ProductFacadeService : IProductFacadeService
    {
        private readonly ProductContext _context;

        public ProductFacadeService(ProductContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(ProductViewModel model)
        {
            var product = new Product(model.Name, model.Description);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task Modify(ProductViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (product == null) return;
            
            product.Update(model.Name, model.Description);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(a => a.Id == id);
            if (product == null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
