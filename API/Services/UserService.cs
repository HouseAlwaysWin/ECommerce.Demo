using System.Threading.Tasks;
using API.Domain.DTO;
using AutoMapper;
using ECommerce.Demo.API.Repositories;
using ECommerce.Demo.Core.Entities;
using ECommerce.Demo.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public UserService (
            IUnitOfWork uow,
            IUserRepository userRepository,
            StoreContext context,
            IMapper mapper) {
            this._mapper = mapper;
            this._context = context;
            this._uow = uow;
            this._userRepository = userRepository;
        }

        public async Task<UserDetailDto> GetUserAsync (int id) {
            var user = await _context.Users.FirstOrDefaultAsync (u => u.Id == id);
            var userDto = _mapper.Map<User, UserDetailDto> (user);

            return userDto;
        }

        

    }
}