using AppointmentMaker.Models;
using AppointmentMaker.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentMaker.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext context { get; set; }

        public PostController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllPosts()
        {
            var allPosts = context.Posts.ToList();
            return View(allPosts);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyPost()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var posts = await context.Posts
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return View(posts);
        }

        //GET-Post,
        [HttpGet]
        public List<PostViewModel> GetTopicPostsForUser(string userId, int topicId)
        {
            var listOFTopicPosts = context.Posts.Where(x => x.TopicId == topicId).ToList();
            var returnList = new List<PostViewModel>(); 
            foreach (var post in listOFTopicPosts)
            {
                var postToAdd = new PostViewModel()
                {
                    Title = post.Title,
                    Body = post.Body,
                    NumberOfLikes = context.PostLikes.Where(x => x.PostId == post.Id).Count(),
                    LikedByCurrentUser = context.PostLikes.Any(x => x.UserId == userId),
                };
                returnList.Add(postToAdd);
            }
            return returnList;
        }

        [HttpPost]
        public IActionResult Create(PostCreateModel model, IFormFile postImage)
        {
            var username = User.Identity.Name;
            var currentUser = context.Users.FirstOrDefault(u => u.UserName == username);
            var topic = context.Topics.FirstOrDefault(t => t.Id == model.TopicId);

            var imageString = string.Empty;
            if (postImage != null && postImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postImage.CopyTo(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    imageString = Convert.ToBase64String(imageBytes);
                }
            }

            var newPost = new Post()
            {
                Title = model.Title,
                Body = model.Body,
                postImageString = imageString,
                UserId = currentUser.Id,
                TopicId = topic.Id,
            };

            context.Add(newPost);
            context.SaveChanges();
             return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public void Delete(string username, int postId)
        {
            var userId = context.Users.Where(x => x.UserName == username).FirstOrDefault().Id;
            var deletePost = context.Posts.Where(x => x.Id == postId).FirstOrDefault();
            if(deletePost != null)
            {
                var currentUserRole = context.UserRoles.Where(x => x.UserId == userId).FirstOrDefault();
                var usersRole = context.Roles.Where(x => x.Id == currentUserRole.RoleId).FirstOrDefault();
                if(deletePost.UserId == userId || usersRole.Name == "Admin") // validacija na nivou bekenda, post moze izbrisati samo admin i kreator posta
                {
                    context.Remove(deletePost);
                }
            }
            //else
            //{
            //    return Error()
            //}
        }
    }
}
