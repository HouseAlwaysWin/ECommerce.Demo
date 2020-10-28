using System.Collections.Generic;
using System.Linq;
using API.Domain.Entities;
using API.Domain.Model;
using API.Repositories;
using API.SqlServerRepo.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace API.Services {
    public class ProductService : IProductService {

        private readonly IConfiguration _config;
        private IProductRepository _sqlServerRepo;
        private IProductRepository _npgSqlRepo;
        private IUnitOfWork _uow;
        public ProductService (IConfiguration config) {
            _config = config;
            _uow = new UnitOfWork<SqlConnection> (_config["ConnectionString:Default"]);
            _sqlServerRepo = new ProductRepository<SqlConnection> (_uow);
        }

        public List<Product> GetProducts (int num = 1000) {
            _uow.BeginTrans ();
            var products = _sqlServerRepo.GetProducts (num).ToList ();
            _uow.Commit ();
            return products;
        }

    }

}