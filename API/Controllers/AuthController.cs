using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Demo.API.Domain.DTO;
using ECommerce.Demo.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Demo.API.Controllers {
    public class AuthController : ControllerBase {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController (
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config,
            IMapper mapper
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> RegisterAsync (UserRegisterDto userRegisterDto) {
            var createUser = _mapper.Map<User> (userRegisterDto);

            var result = await _userManager.CreateAsync (createUser, userRegisterDto.Password);

            if (result.Succeeded) {
                return CreatedAtRoute ("GetUser", new { controller = "Users", id = createUser.Id });
            }
            return BadRequest (result.Errors);
        }

    }
}