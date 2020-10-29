using System.Collections.Generic;
using ECommerce.Demo.API.Domain.Entities;

namespace ECommerce.Demo.API.SqlServerRepo.Repositories {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts (int num = 1000);

    }
}