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

        [HttpGet(Name = "GetMemoDates")]
        public List<MemorableDates> GetMemoDates()
        {
            var dates = _db.MemorableDates.ToList();
            return dates;
        }
        [HttpPut(Name = "PutMemoDates")]
        public void PutMemoDates(int id, DateTime eventDate, string notificationText, string author)
        {
            eventDate = DateTime.SpecifyKind(eventDate, DateTimeKind.Utc);

            MemorableDates MemoDates = new MemorableDates { Id = id, EventDate = eventDate, NotificationText = notificationText, Created = DateTime.UtcNow, Author = author };
            _db.MemorableDates.Add(MemoDates);
            _db.SaveChanges();
        }
        [HttpDelete(Name = "DeleteMemoDates")]
        public void DeleteMemoDates(int id)
        {
            MemorableDates MemoDates = new MemorableDates();
            MemoDates.Id = id;
            _db.MemorableDates.Remove(MemoDates);
            _db.SaveChanges();
        }
    }
}
