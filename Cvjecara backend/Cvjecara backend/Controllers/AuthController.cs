using Cvjecara_backend.DataModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

namespace Cvjecara_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CvjecaraContext _context;
        private readonly IConfiguration _configuration;
        
        public AuthController(IConfiguration configuration, CvjecaraContext context)
        {
            this._configuration = configuration;
            _context = context;
        }
       

        [HttpPost("register")]
        [EnableCors]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            User user = new User();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            user.Email = request.Email;
            user.Name = request.Name;
            user.LastName = request.LastName;
            user.Role = request.Role;
            user.Title = request.Title;
            user.DateOfBirth = request.DateOfBirth;
            user.password = passwordHash;
            
            if (_context.Users == null)
            {
                return Problem("Entity set 'CvjecaraContext.Users'  is null.");
            }
            var duplicateUser = _context.Users.FirstOrDefault(r => r.Email == request.Email);

            if (duplicateUser != null)
            {
                return BadRequest("Vec postoji korisnik sa ovim emailom!!!");

            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            UserDtoResponse userDtoResponse = new UserDtoResponse();
            userDtoResponse.Email = user.Email;
            userDtoResponse.Name = user.Name;
            userDtoResponse.LastName = user.LastName;
            userDtoResponse.Role = user.Role;
            userDtoResponse.Title = user.Title;
            userDtoResponse.DateOfBirth= user.DateOfBirth;
            userDtoResponse.id = user.Id;
            userDtoResponse.token = CreateToken(user);
            
            return Ok(userDtoResponse);

        }



        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDtoLogin request)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault(r => r.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Pogresan email ili sifra!!!");

            }
            if (!BCrypt.Net.BCrypt.Verify(request.password, user.password))
            {
                return BadRequest("Pogresan email ili sifra!!!");
            }
            else
                user.Narudzbas = null;
            
            UserDtoResponse userDtoResponse = new UserDtoResponse();
            userDtoResponse.Email = user.Email;
            userDtoResponse.Name = user.Name;
            userDtoResponse.LastName = user.LastName;
            userDtoResponse.Role = user.Role;
            userDtoResponse.Title = user.Title;
            userDtoResponse.DateOfBirth = user.DateOfBirth;
            userDtoResponse.token = CreateToken(user);

            return Ok(userDtoResponse);

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:JWTToken").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims,expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var jwt  = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
