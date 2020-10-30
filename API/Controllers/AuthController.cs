using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Domain.DTO;
using AutoMapper;
using ECommerce.Demo.API.Domain.DTO;
using ECommerce.Demo.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<IActionResult> Login (UserLoginDto userLoginDto) {
            var user = await _userManager.FindByNameAsync (userLoginDto.Username);
            var result = await _signInManager.CheckPasswordSignInAsync (user, userLoginDto.Password, false);

            if (result.Succeeded) {
                var appUser = _mapper.Map<UserDetailDto> (user);
                return Ok (new {
                    token = GenerateJwtToken (user).Result, user = appUser
                });
            }
            return Unauthorized ();
        }

        private async Task<string> GenerateJwtToken (User user) {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync (user);

            foreach (var role in roles) {
                claims.Add (new Claim (ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey (
                Encoding.UTF8.GetBytes (_config.GetSection ("AppSettings:Token").Value));

            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);
            return tokenHandler.WriteToken (token);
        }

    }
}