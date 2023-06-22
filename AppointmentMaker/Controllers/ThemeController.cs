using AppointmentMaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AppointmentMaker.Controllers
{
    public class ThemeController : Controller
    {

        private readonly ILogger<ThemeController> _logger;
        public ApplicationDbContext context { get; set; }

        public ThemeController(ILogger<ThemeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Create(ThemeCreateModel model)
        {
            var newTopic = new Topic
            {
                Name = model.ThemeName,
                Description = model.ThemeDescription,
                EcodedImage = model.EncodedImage,
            };
            context.Add(newTopic);
            context.SaveChanges();
        }

        [HttpDelete]
        public void Delete(int postId)
        {
           var topicToRemove = context.Topics.Where(x => x.Id == postId).FirstOrDefault();
            if(topicToRemove != null)
            {
                context.Topics.Remove(topicToRemove);
                context.SaveChanges();
            }
        }
    }
}
