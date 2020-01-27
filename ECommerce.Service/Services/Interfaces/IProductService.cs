using System.Collections.Generic;

namespace ECommerce.Service.Services.Interfaces
{
    public interface IProductService<T>
    {
        IEnumerable<T> GetProducts();
        T GetProductById(string Id);

    }
}