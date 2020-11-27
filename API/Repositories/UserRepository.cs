
using ECommerce.Demo.Core.Entities;

namespace ECommerce.Demo.API.Repositories {
    public class UserRepository<DbConn> : GenericRepository<User>, IUserRepository {
        public UserRepository (IUnitOfWork uow) : base (uow) { }
    }
}