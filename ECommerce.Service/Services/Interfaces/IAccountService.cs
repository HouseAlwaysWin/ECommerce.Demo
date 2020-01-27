using System.Threading.Tasks;
using ECommerce.Domain.Models.Account;

namespace ECommerce.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultModel> LoginAsync(LoginModel model);
        Task<RegisterResultModel> RegisterAsync(RegisterModel model);
        Task LogoutAsync();
    }
}