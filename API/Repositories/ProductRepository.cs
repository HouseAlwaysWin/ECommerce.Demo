using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using API.Domain.Entities;
using API.Repositories;
using Dapper;

namespace API.SqlServerRepo.Repositories {

    public class ProductRepository<DbConn> : IProductRepository where DbConn : IDbConnection, new () {

        private IUnitOfWork _uow;
        public ProductRepository (IUnitOfWork uow) {
            _uow = uow;
        }

        public IEnumerable<Product> GetProducts (int num = 1000) {
            string sqlCmd = GetSqlCommand ().GetProducts ();
            return _uow.Connection.Query<Product> (sqlCmd, new { Num = num }, _uow.Transaction);
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