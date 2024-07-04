using Microsoft.AspNetCore.Mvc;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {

        private readonly ILogger<NewsController> _logger;
        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        /*[HttpGet(Name = "GetNews")]
        public IEnumerable<News> Get(DateTime startDate, DateTime endDate, string title, string content, int importance, string author)
        {
            return Enumerable.Range(1, 5).Select(index => new News
            {
                StartDate = startDate,
                EndDate = DateTime.Parse("2024-07-02T20:15:29.327Z"),
                Title = title,
                Content = content,
                Importance = importance,
                Created = DateTime.Now,
                Author = author,
            })
            .ToArray();

        }*/
        [HttpGet(Name = "GetNews")]
        public List<User> GetUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.ToList();
                return users;
            }
        }
        [HttpPut(Name = "PutNews")]
        public News PutUser(int id, DateTime startDate, DateTime endDate, string title, string content, int importance, string author)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                News news = new News { Id = id, StartDate = startDate, EndDate = endDate, Title = title, Content = content, Importance = importance,Created = DateTime.UtcNow, Author = author };
                db.News.Add(news);
                db.SaveChanges();
                return news;
            }
        }

    }
}
