using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using API.Domain.Entities;
using API.Repositories;
using API.Repositories.SqlCommands;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.SqlServerRepo.Repositories {

    public class ProductRepository<DbConn> : IProductRepository where DbConn : IDbConnection, new () {
        internal class ProductSqlServerCmd : ISqlCommand {
            public string GetProducts () {
                return @"";
            }
        }

        private IUnitOfWork _uow;
        public ProductRepository (IUnitOfWork uow) {
            _uow = uow;
        }

        public IEnumerable<Product> GetProducts (int num = 1000) {
            var name = typeof (DbConn).FullName.ToLower ();
            string sqlCmd = string.Empty;
            switch (name) {
                case "npgsqlconnection":
                    sqlCmd = @"";
                    break;
                default:
                    sqlCmd = @"";
                    break;
            }

            return _uow.Connection.Query<Product> (sqlCmd, new { Num = num }, _uow.Transaction);
        }

    }
}