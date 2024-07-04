using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet(Name = "GetUsers")]
        public List<User> GetUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();
                return users;
            }
        }

        [HttpPut(Name = "PutUser")]
        public User PutUser(int id, string login, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user1 = new User { Id = id, Login = login, Password = password};
                db.Users.Add(user1);
                db.SaveChanges();
                return user1;
            }
        }
        [HttpDelete(Name = "DeleteUser")]
        public User PutUsers(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = new User();
                user.Id = id;
                if(id != null)
                {
                    db.Users.Remove(user);
                }
                db.SaveChanges();
                return user;
            }
        }

        /*[HttpPost(Name = "LoadUser")]
        PublicKey 
        using (ApplicationContext db = new ApplicationContext())
        {
            User user1 = new User { Id = 1, Login = "Alice", Password = "Alice123" };
            User user2 = new User { Id = 2, Login = "Rachel", Password = "123" };

            db.Users.AddRange(user1, user2);
            db.SaveChanges();
        }*/

    }
}
