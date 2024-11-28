using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NWU_Taskmaster.Models;

namespace NWU_Taskmaster.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index(int? categoryId)
        {
            // Fetch categories dynamically for the filter
            ViewBag.Categories = db.TaskCategories
                .Select(c => new SelectListItem
                {
                    Value = c.category_id.ToString(),
                    Text = c.category_name
                }).ToList();

            // Get the aspnet_user_id of the logged-in user
            var aspnetUserId = User.Identity.GetUserId();

            // Retrieve the client_id for the current user
            var client = db.Clients.FirstOrDefault(c => c.aspnet_user_id == aspnetUserId);

            if (client == null)
            {
                // Handle the case where no client record is found
                return HttpNotFound("Client record not found for the logged-in user.");
            }

            var clientId = client.client_id;

            // Fetch tasks for the logged-in user
            var tasks = db.ClientTasks.Where(t => t.client_id == clientId);

            // Filter tasks by category if a category is selected
            if (categoryId.HasValue && categoryId > 0)
            {
                tasks = tasks.Where(t => t.category_id == categoryId);
            }

            return View(tasks.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}