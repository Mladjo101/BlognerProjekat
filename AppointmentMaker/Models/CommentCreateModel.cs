namespace AppointmentMaker.Models
{
    public class CommentCreateModel
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
    }
}
