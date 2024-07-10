using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        [HttpGet(Name = "GetUsers")]
        public List<User> GetUsers()
        {
            var users = _db.Users.ToList();
            return users;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string login, string password)
        {
            User user = new User {Login = login, Password = password};
            if (_db.Users.Any(u => u.Login == user.Login))
            {
                return BadRequest("User already exists.");
            }

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        [HttpDelete(Name = "DeleteUser")]
        public void DeleteUser(int id)
        {
            User user = new User();
            user.Id = id;

            _db.Users.Remove(user);
            _db.SaveChanges();
        }

       

        [HttpPost("login")]
        public IActionResult Login(string? login, string? password)
        {
            var User = _db.Users.Where(u => u.Login == login && u.Password == password);
            if (User == null)
            {
                return Unauthorized("Invalid login or password");
            }

            return Ok(new { Token = "token" }); // Здесь можно вернуть JWT токен при необходимости
        }
    }
}
