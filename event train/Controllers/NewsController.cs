using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace event_train.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public NewsController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet("GetNews")]
        public async Task<ActionResult<List<News>>> GetNews()
        {
            var news = await _db.News.ToListAsync();
            return news;
        }

        [HttpGet("date")]
        public async Task<ActionResult> GetNewsByDate(DateTime date)
        {
            var news = await _db.News
                .Where(n => n.StartDate >= date && date <= n.EndDate)
                .ToListAsync();
            var memodates = await _db.MemorableDates
                .Where(s => s.EventDate == date)
                .Select(s => s.NotificationText)
                .ToListAsync();

            var combine = new Combine
            {
                News = news,
                MemorableDates = memodates
            };

            return Ok(combine);
        }
        public class Combine
        {
            public List<News> News { get; set; }
            public List<string> MemorableDates { get; set; }
        }

        [HttpPut(Name = "PutNews")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PutNews(DateTime startDate, DateTime endDate, string title, string content, int importance, string author)
        {
            /*      2020/02/02 00:00:00          */
            News news = new News
            {
                StartDate = startDate,
                EndDate = endDate,
                Title = title,
                Content = content,
                Importance = importance,
                Created = DateTime.Now,
                Author = author
            };
            _db.News.Add(news);
            await _db.SaveChangesAsync();
            return Ok(news);
        }

        [HttpDelete(Name = "DeleteNews")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            News news = new News { Id = id };

            _db.News.Remove(news);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
