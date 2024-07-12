using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemoDatesController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public MemoDatesController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet("date")]
        public async Task<ActionResult> GetMemoDates(DateTime date)
        {
            var news = await _db.MemorableDates
                .Where(s => s.EventDate == date)
                .ToListAsync();

            return Ok(news);
        }

        [HttpPut("id")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> PutMemoDates(DateTime eventDate, string notificationText, string author)
        {
            MemorableDates MemoDates = new MemorableDates
            {
                EventDate = eventDate,
                NotificationText = notificationText,
                Created = DateTime.Now,
                Author = author
            };

            _db.MemorableDates.Add(MemoDates);
            await _db.SaveChangesAsync();

            return Ok(MemoDates);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteMemoDates(int id)
        {
            MemorableDates MemoDates = new MemorableDates {Id = id };
            _db.MemorableDates.Remove(MemoDates);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
