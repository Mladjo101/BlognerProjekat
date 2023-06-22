using AppointmentMaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AppointmentMaker.Controllers
{
    public class CommentController : Controller
    {

        private readonly ILogger<CommentController> _logger;
        public ApplicationDbContext context { get; set; }

        public CommentController(ILogger<CommentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Create(CommentCreateModel model)
        {
            var newComment = new Comment
            {
                PostId = model.PostId,
                Conntent = model.Content,
                User = context.Users.Where(x => x.UserName == model.Username).FirstOrDefault(),
            };
            context.Add(newComment);
            context.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int commentId, string username)
        {

            var userId = context.Users.Where(x => x.UserName == username).FirstOrDefault().Id;

            var currentUserRole = context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
            var usersRole = context.Roles.Where(x => x.Id == currentUserRole.RoleId).FirstOrDefault();
            var commentToRemove = context.Comments.Where(x => x.Id == commentId).FirstOrDefault();
            if (commentToRemove != null)
            {
                if (commentToRemove.UserId == userId || usersRole.NormalizedName == "Admin")
                {
                    context.Comments.Remove(commentToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}
