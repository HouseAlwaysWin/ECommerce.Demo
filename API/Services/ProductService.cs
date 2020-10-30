using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Demo.API.Domain.Entities;
using ECommerce.Demo.API.Repositories;
using ECommerce.Demo.API.SqlServerRepo.Repositories;
using NLog;

namespace ECommerce.Demo.API.Services {
    public class ProductService : IProductService {
        private IProductRepository _repo;
        private IUnitOfWork _uow;
        private static Logger _logger = NLog.LogManager.GetCurrentClassLogger ();
        public ProductService (IUnitOfWork uow, IProductRepository repo) {
            _uow = uow;
            _repo = repo;
        }

        public List<Product> GetProducts (int num = 1000) {
            try {
                _uow.BeginTrans ();
                var products = _repo.GetProducts (num).ToList ();
                _uow.Commit ();
                return products;
            } catch (Exception ex) {
                _logger.Error (ex, "Get Products  Error");
            }
            return null;
        }

    }

}