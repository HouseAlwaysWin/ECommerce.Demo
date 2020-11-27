using System.Threading.Tasks;
using API.Domain.DTO;

namespace API.Services {
    public interface IUserService {
        Task<UserDetailDto> GetUserAsync (int id);
    }
}