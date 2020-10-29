using System.Collections.Generic;
using API.Extensions;
using Dapper.Contrib.Extensions;

namespace API.Repositories {
    public class GenericRepository<T> where T : class {
        private IUnitOfWork _uow;
        public GenericRepository (IUnitOfWork uow) {
            _uow = uow;
        }

        public IEnumerable<T> GetPaginated (ref int total, int currentPage, int itemsPerPage) {
            return _uow.Connection.GetPaginated<T> (ref total, currentPage, itemsPerPage, _uow.Transaction);
        }

        public IEnumerable<T> GetAll () {
            return _uow.Connection.GetAll<T> (_uow.Transaction);
        }

        public T Get (object id) {
            return _uow.Connection.Get<T> (id, _uow.Transaction);
        }

        public void Insert (T model) {
            _uow.Connection.Insert<T> (model, _uow.Transaction);
        }

        public void Update (T model) {
            _uow.Connection.Update<T> (model, _uow.Transaction);
        }

        public void Delete (T model) {
            _uow.Connection.Delete<T> (model, _uow.Transaction);
        }

        public void DeleteAll () {
            _uow.Connection.DeleteAll<T> (_uow.Transaction);
        }

    }
}