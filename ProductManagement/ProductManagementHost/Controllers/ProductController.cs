using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Facade.Contracts.Products.Services;
using ProductManagement.Facade.Contracts.Products.ViewModels;

namespace ProductManagementHost.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductFacadeService _facadeService;

        public ProductController(IProductFacadeService facadeService)
        {
            _facadeService = facadeService;
        }
        
        [HttpPost]
        public async Task<Guid> Post(ProductViewModel model)
        {
            return await _facadeService.Create(model);
        }

        [HttpPut("{id:guid}")]
        public async Task Put(Guid id, ProductViewModel model)
        {
            model.Id = id;
            await _facadeService.Modify(model);
        }

        [HttpDelete("{id:guid}")]
        public async Task Delete(Guid id)
        {
            await _facadeService.Delete(id);
        }
    }
}