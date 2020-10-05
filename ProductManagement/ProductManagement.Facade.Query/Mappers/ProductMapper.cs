using System.Collections.Generic;
using System.Linq;
using ProductManagement.Domain;
using ProductManagement.Facade.Contracts.Products.DTOs;

namespace ProductManagement.Facade.Query.Mappers
{
    internal static class ProductMapper
    {
        internal static ProductDto Map(Product model)
        {
            if (model == null) return null;
            return new ProductDto
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
        internal static List<ProductDto> Map(List<Product> list)
        {
            return list.Select(Map).ToList();
        }
    }
}
