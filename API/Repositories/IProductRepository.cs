using System.Collections.Generic;
using API.Domain.Entities;

namespace API.SqlServerRepo.Repositories {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts (int num = 1000);

    }
}