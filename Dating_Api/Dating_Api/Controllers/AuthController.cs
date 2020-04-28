using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dating_Api.Controllers.DTOS;
using Dating_Api.Data;
using Dating_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
    

namespace Dating_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo,IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // validate request
           userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.UserExist(userForRegisterDto.Username))
            {
                return BadRequest("User Already Exists");
            }

            //create new user after successfully registering

            var usertocreate = new User
            {

                username = userForRegisterDto.Username

            };

            var createduser = await _repo.Register(usertocreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPost("login")]

        public async Task<IActionResult> Login(UserforLoginDTO serforLoginDTO)
        {
            var userFromRepo = await _repo.Login(serforLoginDTO.Username.ToLower(), serforLoginDTO.Password);
            if(userFromRepo==null)
            {
                return Unauthorized();
            }

            // Token Claim will contain user id and user name

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.ID.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

    }
}
