using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Facade.Contracts.Products.DTOs;
using ProductManagement.Facade.Contracts.Products.Services;

namespace ProductManagementHost.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductQueryController : ControllerBase
    {
        private readonly IProductFacadeQuery _facadeQuery;

        public ProductQueryController(IProductFacadeQuery facadeQuery)
        {
            _facadeQuery = facadeQuery;
        }

        [HttpGet]
        public async Task<IList<ProductDto>> Get()
        {
            return await _facadeQuery.GetAll();
        }

        [HttpGet("{id:guid}")]
        public async Task<ProductDto> Get(Guid id)
        {
            return await _facadeQuery.GetById(id);
        }
    }
}
