using Microsoft.AspNetCore.Mvc;

namespace event_train.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemoDatesController : Controller
    {
        [HttpGet(Name = "GetMemoDates")]
        public List<MemorableDates> GetMemoDates()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var dates = db.MemorableDates.ToList();
                return dates;
            }
        }
        [HttpPut(Name = "PutMemoDates")]
        public void PutMemoDates(int id, DateTime eventDate, string notificationText, string author)
        {
            eventDate = DateTime.SpecifyKind(eventDate, DateTimeKind.Utc);

            using (ApplicationContext db = new ApplicationContext())
            {
                MemorableDates MemoDates = new MemorableDates { Id = id, EventDate = eventDate, NotificationText = notificationText, Created = DateTime.UtcNow, Author = author };
                db.MemorableDates.Add(MemoDates);
                db.SaveChanges();
            }
        }
        [HttpDelete(Name = "DeleteMemoDates")]
        public void DeleteMemoDates(int id)
        {
            MemorableDates MemoDates = new MemorableDates();
            MemoDates.Id = id;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.MemorableDates.Remove(MemoDates);
                db.SaveChanges();
            }
        }
    }
}
