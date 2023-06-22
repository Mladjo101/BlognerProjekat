using AppointmentMaker;
using AppointmentMaker.Models;
using AppointmentMaker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AppointmentMaker.Controllers
{
    public class UserManagmentController : Controller
    {
        private readonly ILogger<UserManagmentController> _logger;
        public ApplicationDbContext context { get; set; }

        public UserManagmentController(ILogger<UserManagmentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public IActionResult Index()
        {

			IEnumerable<ApplicationUser> objList = (from user in context.Users
                                                    join userRole in context.UserRoles
													on user.Id equals userRole.UserId
                                                    join role in context.Roles
                                                    on userRole.RoleId equals role.Id
                                                    where role.Name == "User"
                                                    select user);
			return View(objList);
		}

		//Post-Delete
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePost(string? id)
		{
			
			var obj = context.Users.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			context.Users.Remove(obj);
			context.SaveChanges();
			return RedirectToAction("Index");

		}
		//Get-Delete
		public IActionResult Delete(string? id)
		{

			if (id == null)
			{
				return NotFound();
			}
			var obj = context.Users.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);

		}

		//get-update
		public IActionResult Update(string? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var obj = context.Users.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);

		}

		//POST UPDATE
		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult UpdatePost(ApplicationUser obj)
		{
			if (ModelState.IsValid)
			{
                obj.UserName = obj.Name;
				obj.NormalizedUserName = obj.Name;
                context.Users.Update(obj);
				
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}


		/*[HttpDelete]
		public void Delete(string userId)
		{
			var userToRemove = context.Users.Where(x => x.Id == userId).FirstOrDefault();
			if (userToRemove != null)
			{
				context.Users.Remove(userToRemove);
				context.SaveChanges();
			}
		}
		*/

	}
}
