using NpgsqlTypes;

namespace event_train
{
    public class News
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Importance { get; set; }
        public DateTime Created { get; set; }
        public string? Author { get; set; }
    }
}