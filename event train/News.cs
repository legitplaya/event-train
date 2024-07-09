namespace event_train
{
    public class News
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Importance { get; set; }
        public DateOnly Created { get; set; }
        public string? Author { get; set; }
    }
}