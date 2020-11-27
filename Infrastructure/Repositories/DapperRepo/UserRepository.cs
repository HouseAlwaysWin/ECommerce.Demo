
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Core.Interfaces;

namespace ECommerce.Demo.Infrastructure.Repositories.DapperRepo {
    public class UserRepository<DbConn> : GenericRepository<User>, IUserRepository {
        public UserRepository (IUnitOfWork uow) : base (uow) { }
    }
}