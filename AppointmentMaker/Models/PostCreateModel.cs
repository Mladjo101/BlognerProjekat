namespace AppointmentMaker.Models
{
    public class PostCreateModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public string? PostImageString { get; set; }
        public int TopicId { get; set; }
    }
}
