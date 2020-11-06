using System.Threading.Tasks;
using API.Domain.DTO;
using ECommerce.Demo.API.Domain.Entities;

namespace API.Services {
    public interface IUserService {
        Task<UserDetailDto> GetUserAsync (int id);
    }
}