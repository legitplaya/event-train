namespace event_train
{
    public class MemorableDates
    {
        public int Id { get; set; }
        public DateOnly EventDate { get; set; }
        public string? NotificationText { get; set; }
        public DateOnly Created { get; set; }
        public string? Author { get; set; }
    }
}
