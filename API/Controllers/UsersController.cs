using System.Threading.Tasks;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [AllowAnonymous]
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserService _userService;
        public UsersController (IUserService userService) {
            this._userService = userService;
        }

        [HttpGet ("GetUser/{id}")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _userService.GetUserAsync (1);
            return Ok (user);
        }

    }
}