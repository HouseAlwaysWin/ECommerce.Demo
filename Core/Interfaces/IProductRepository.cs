using System.Collections.Generic;
using ECommerce.Demo.Core.Entities;

namespace ECommerce.Demo.Core.Interfaces {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts (int num = 1000);

    }
}