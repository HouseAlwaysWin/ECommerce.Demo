using System;
using System.Data;

namespace API.Repositories {
    public interface IUnitOfWork : IDisposable {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void BeginTrans ();
        void Commit ();
        void Rollback ();
    }
}