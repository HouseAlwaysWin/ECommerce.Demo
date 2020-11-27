using System.Transactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using ECommerce.Demo.Core.Interfaces;

namespace ECommerce.Demo.Infrastructure.Repositories.DapperRepo {

    public class UnitOfWork<T> : IUnitOfWork where T : IDbConnection, new () {

        private bool _disposed;
        private T _connection;
        private IDbTransaction _transaction;
        public IDbConnection Connection { get { return _connection; } }
        public IDbTransaction Transaction { get { return _transaction; } }

        public UnitOfWork (string connectionString) {
                _connection = new T ();
                _connection.ConnectionString = connectionString;
                _connection.Open ();
        }

        public void Commit () {
            try {
                _transaction.Commit ();
            } catch {
                _transaction.Rollback ();
            } finally {
                _transaction.Dispose ();
            }
        }



        public void Dispose () {
            dispose (true);
            GC.SuppressFinalize (this);
        }

        private void dispose (bool disposing) {
            if (!_disposed) {
                if (disposing) {
                    if (_transaction != null) {
                        _transaction.Dispose ();
                        _transaction = null;
                    }
                    if (_connection != null) {
                        _connection.Dispose ();
                        _connection = default (T);
                    }
                }
                _disposed = true;
            }
        }

        public void BeginTrans () {
            _transaction = _connection.BeginTransaction ();
        }

        public void Rollback () {
            _transaction.Rollback ();
            _transaction.Dispose ();
        }

        ~UnitOfWork () {
            dispose (false);
        }

    }
}