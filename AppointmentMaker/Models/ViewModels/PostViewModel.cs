namespace AppointmentMaker.Models.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int NumberOfLikes { get; set; }
        public bool LikedByCurrentUser { get; set; }
    }
}
