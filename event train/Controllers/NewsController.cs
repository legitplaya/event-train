using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace event_train.Controllers
{
    /*[ApiController]*/
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public NewsController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet("date")]
        public async Task<IActionResult> GetNewsByDate(string date)
        {
            var news = await _db.News
                .Where(s => s.Created == DateOnly.Parse(date))
                .ToListAsync();
            return Ok(news);
        }

        [HttpPost(Name = "PutNews")]
        public async  Task<ActionResult> PutNews(int id, string startDate, string endDate, string title, string content, int importance, string author)
        {
            News news = new News
            {
                Id = id,
                StartDate = DateOnly.Parse(startDate),
                EndDate = DateOnly.Parse(endDate),
                Title = title,
                Content = content,
                Importance = importance,
                Created = DateOnly.FromDateTime(DateTime.Now),
                Author = author
            };
            _db.News.Add(news);
            await _db.SaveChangesAsync();
            return Ok(news);
        }

        [HttpDelete(Name = "DeleteNews")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            News news = new News {Id = id };

            _db.News.Remove(news);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
