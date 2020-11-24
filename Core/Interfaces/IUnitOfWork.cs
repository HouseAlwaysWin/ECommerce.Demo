using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Demo.Core.Interfaces {
    public interface IUnitOfWork : IDisposable {

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTrans ();
        void Commit ();
        void Rollback ();
    }
}