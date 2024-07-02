using Microsoft.AspNetCore.Mvc;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "МИХАИЛ ИГНАТЕЬВ ", "ЧЕБОКСАРСКИЙ ФРАНЦУЗ ","МАРИНА АНАТОЛЬЕВНА "
        };

        private readonly ILogger<NewsController> _logger;
        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetNews")]
        public IEnumerable<News> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new News
            {
                StartDate = DateTime.Now.AddDays(index),
                EndDate = DateTime.Parse("2024-07-02T20:15:29.327Z"),
                Title = "ЧЕБОКСАРЫ ВЗОРВАЛИСЬ",
                Content = "В ЧБЕБОКСАРАХ ВЗОРВАЛИСЬ ОТ СМЕХА НАРОД НА ПЛОЩАДИ КРАСНОЙ УХАХХАХАХАХ)))))",
                Importance = 0,
                Created = DateTime.Now,
                Author = Summaries[Random.Shared.Next(Summaries.Length)],
            })
            .ToArray();
            
        }
    }
}
