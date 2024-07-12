using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext context)
        {
            _db = context;
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet(Name = "GetUsers")]
        public List<User> GetUsers()
        {
            var users = _db.Users.ToList();
            return users;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string login, string password)
        {
            User user = new User { Login = login, Password = password };

            if (_db.Users.Any(u => u.Login == user.Login))
            {
                return BadRequest("User already exists.");
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("User registered successfully.");
        }


        [HttpDelete(Name = "DeleteUser")]
        [Authorize(Roles = "admin")]
        public void DeleteUser(int id)
        {
            User user = new User { Id = id };

            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        [HttpPost("login")]
        public IActionResult Login(string? login, string? password)
        {
            var User = _db.Users.SingleOrDefault(u => u.Login == login && u.Password == password);
            if (User == null)
            {
                return Unauthorized("Invalid login or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Login),
                new Claim(ClaimTypes.Role, User.Role) // Добавляем роль в токен
            };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new { Token = token });
        }
        [HttpPost("give role")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GiveRole(string login, string role)
        {
            var user = _db.Users.SingleOrDefault(u => u.Login == login);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Role = role;
            await _db.SaveChangesAsync();

            return Ok("Role given");
        }
    }
}
