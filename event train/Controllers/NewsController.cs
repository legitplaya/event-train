using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public NewsController(ApplicationContext context)
        {
            _db = context;
        }

        /*        private readonly ILogger<NewsController> _logger;
                public NewsController(ILogger<NewsController> logger)
                {
                    _logger = logger;
                }*/

        [HttpGet(Name = "GetNews")]
        public List<News> GetNews()
        {
            var news = _db.News.ToList();
            return news;
        }

        [HttpPut(Name = "PutNews")]
        public void PutNews(int id, DateTime startDate, DateTime endDate, string title, string content, int importance, string author)
        {
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            News news = new News { Id = id, StartDate = startDate, EndDate = endDate, Title = title, Content = content, Importance = importance, Created = DateTime.UtcNow, Author = author };
            _db.News.Add(news);
            _db.SaveChanges();
        }

        [HttpDelete(Name = "DeleteNews")]
        public void DeleteNews(int id)
        {
            News news = new News();
            news.Id = id;

            _db.News.Remove(news);
            _db.SaveChanges();
        }
    }
}
