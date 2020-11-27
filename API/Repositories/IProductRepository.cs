using System.Collections.Generic;
using ECommerce.Demo.Core.Entities;

namespace ECommerce.Demo.API.SqlServerRepo.Repositories {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts (int num = 1000);

    }
}