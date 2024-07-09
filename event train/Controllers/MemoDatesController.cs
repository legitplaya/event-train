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
        public ActionResult<List<MemorableDates>> GetMemoDates()
        {
            var dates = _db.MemorableDates.ToList();
            return dates;
        }
        [HttpPut("{id}")]
        public void PutMemoDates(int id, DateOnly eventDate, string notificationText, string author)
        {
            MemorableDates MemoDates = new MemorableDates
            {
                Id = id,
                EventDate = eventDate,
                NotificationText = notificationText,
                Created = DateOnly.FromDateTime(DateTime.Now),
                Author = author
            };

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
