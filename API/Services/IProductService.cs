using System.Collections.Generic;
using ECommerce.Demo.Core.Entities;

namespace ECommerce.Demo.API.Services {
    public interface IProductService {
        List<Product> GetProducts (int num = 1000);
    }
}