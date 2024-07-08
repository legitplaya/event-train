using Microsoft.EntityFrameworkCore;

namespace event_train
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<MemorableDates> MemorableDates { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }
    }
}
