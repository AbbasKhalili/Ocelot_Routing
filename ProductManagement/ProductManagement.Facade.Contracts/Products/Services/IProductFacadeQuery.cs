using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagement.Facade.Contracts.Products.DTOs;

namespace ProductManagement.Facade.Contracts.Products.Services
{
    public interface IProductFacadeQuery : IFacadeService
    {
        Task<ProductDto> GetById(Guid id);
        Task<List<ProductDto>> GetAll();
    }
}