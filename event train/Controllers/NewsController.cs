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

        [HttpGet("date")]
        public async Task<IActionResult> GetNewsByDate(DateTime date)
        {
            var news = await _db.News
                .Where(s => s.Created == date)
                .ToListAsync();
            return Ok(news);
        }

        [HttpPost(Name = "PutNews")]
        public async Task<ActionResult> PutNews(DateTime startDate, DateTime endDate, string title, string content, int importance, string author)
        {
       /*      2024/07/10 18:15:16          */
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
        public async Task<IActionResult> DeleteNews(int id)
        {
            News news = new News {Id = id };

            _db.News.Remove(news);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
