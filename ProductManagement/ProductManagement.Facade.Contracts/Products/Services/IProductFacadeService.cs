using System;
using System.Threading.Tasks;
using ProductManagement.Facade.Contracts.Products.ViewModels;

namespace ProductManagement.Facade.Contracts.Products.Services
{
    public interface IProductFacadeService : IFacadeService
    {
        Task<Guid> Create(ProductViewModel model);
        Task Modify(ProductViewModel model);
        Task Delete(Guid id);

    }
}
