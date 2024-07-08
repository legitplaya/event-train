using Microsoft.AspNetCore.Mvc;
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

        [HttpPut(Name = "PutUser")]
        public void PutUser(int id, string login, string password)
        {
            User user1 = new User { Id = id, Login = login, Password = password };
            _db.Users.Add(user1);
            _db.SaveChanges();
        }
        [HttpDelete(Name = "DeleteUser")]
        public void DeleteUser(int id)
        {
            User user = new User();
            user.Id = id;

            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
