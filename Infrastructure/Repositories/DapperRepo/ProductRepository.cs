using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerce.Demo.API.Repositories.DapperRepo;
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Core.Interfaces;

namespace ECommerce.Demo.API.SqlServerRepo.Repositories.DapperRepo {

    public class ProductRepository<DbConn> : GenericRepository<Product>, IProductRepository where DbConn : IDbConnection, new () {

        private IUnitOfWork _uow;
        public ProductRepository (IUnitOfWork uow) : base (uow) {
            _uow = uow;
        }

        public IEnumerable<Product> GetProducts (int num = 1000) {
            string sqlCmd = GetSqlCommand ().GetProducts ();
            return _uow.Connection.Query<Product> (sqlCmd, new { Num = num }, _uow.Transaction);
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            throw new System.NotImplementedException();
        }


        #region SqlCmd String
        private interface ISqlCommand {
            string GetProducts ();
        }

        private class SqlServerCmd : ISqlCommand {
            public string GetProducts () {
                return @"";
            }
        }

        private class PostgreCmd : ISqlCommand {
            public string GetProducts () {
                return @"";
            }
        }
        private class MySqlCmd : ISqlCommand {
            public string GetProducts () {
                return @"";
            }
        }

        private readonly Dictionary<string, ISqlCommand> CmdDict = new Dictionary<string, ISqlCommand> {
            ["sqlconnection"] = new SqlServerCmd (),
            ["npgsqlconnection"] = new PostgreCmd (),
            ["mysqlconnection"] = new MySqlCmd (),
        };
        private readonly ISqlCommand DefaultAdapter = new SqlServerCmd ();

        private ISqlCommand GetSqlCommand () {
            var name = typeof (DbConn).FullName.ToLower ();
            return CmdDict.TryGetValue (name, out var cmd) ? cmd : DefaultAdapter;
        }

        #endregion

    }
}