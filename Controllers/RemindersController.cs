using System;
using System.Linq;
using System.Web.Mvc;
using NWU_Taskmaster.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NWU_Taskmaster.Controllers
{
    public class RemindersController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Reminders/Index
        public ActionResult Index(int? taskId)
        {
            var userId = User.Identity.GetUserId();
            var clientId = db.Clients.FirstOrDefault(c => c.aspnet_user_id == userId)?.client_id;

            if (clientId == null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            var tasks = db.ClientTasks.Where(t => t.client_id == clientId).ToList();

            if (!tasks.Any())
            {
                TempData["Error"] = "You need to create a task before adding reminders.";
                return RedirectToAction("Dashboard", "Home");
            }

            if (!taskId.HasValue)
            {
                taskId = tasks.FirstOrDefault()?.task_id; // Default to the first task if none is selected
            }

            var model = new ReminderViewModel
            {
                TaskId = taskId.Value
            };

            ViewBag.Tasks = tasks.Select(t => new SelectListItem
            {
                Value = t.task_id.ToString(),
                Text = t.title
            }).ToList();

            return View(model);
        }

        // GET: /Reminders/Create
        [HttpGet]
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var clientId = db.Clients.FirstOrDefault(c => c.aspnet_user_id == userId)?.client_id;

            if (clientId == null)
            {
                TempData["Error"] = "Unable to identify the client.";
                return RedirectToAction("Index", "Dashboard"); // Redirect if client is not found
            }

            // Populate the task dropdown
            ViewBag.Tasks = db.ClientTasks
                .Where(t => t.client_id == clientId)
                .Select(t => new SelectListItem
                {
                    Value = t.task_id.ToString(),
                    Text = t.title
                }).ToList();

            var model = new ReminderViewModel
            {
                TaskId = 0, // Default to no pre-selected task
                ReminderDate = DateTime.Now // Initialize to current date and time
            };

            return View("Index", model);
        }

        // POST: /Reminders/SaveReminder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveReminder(ReminderViewModel model, string ReminderTime)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate the task dropdown in case of errors
                var userId = User.Identity.GetUserId();
                var clientId = db.Clients.FirstOrDefault(c => c.aspnet_user_id == userId)?.client_id;
                ViewBag.Tasks = db.ClientTasks
                    .Where(t => t.client_id == clientId)
                    .Select(t => new SelectListItem
                    {
                        Value = t.task_id.ToString(),
                        Text = t.title
                    }).ToList();

                return View("Create", model); // Use "Create" view for errors
            }

            // Combine the date and time inputs
            if (DateTime.TryParse($"{model.ReminderDate:yyyy-MM-dd} {ReminderTime}", out DateTime fullReminderDate))
            {
                model.ReminderDate = fullReminderDate; // Ensure this has both date and time
            }
            else
            {
                ModelState.AddModelError("ReminderDate", "Invalid date or time format.");
                return View("Index", model);
            }

            // Save the reminder
            var reminder = new Reminders
            {
                task_id = model.TaskId,
                reminder_date = model.ReminderDate,
                is_recurring = model.IsRecurring,
                recurring_interval = model.RecurringInterval,
                relative_to_due_date = model.RelativeToDueDate,
                relative_time = model.RelativeTime,
                created_at = DateTime.Now
            };

            db.Reminders.Add(reminder);
            db.SaveChanges();

            TempData["Success"] = "Reminder created successfully!";
            return RedirectToAction("Index", "Dashboard");
        }

        public List<Reminders> GetUpcomingReminders(int clientId)
        {
            return db.Reminders
                .Where(r => r.task_id == clientId && r.reminder_date <= DateTime.Now)
                .OrderBy(r => r.reminder_date)
                .ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClearReminder(int reminderId)
        {
            var reminder = db.Reminders.FirstOrDefault(r => r.reminder_id == reminderId);
            if (reminder == null)
            {
                return HttpNotFound();
            }

            db.Reminders.Remove(reminder);
            db.SaveChanges();

            return RedirectToAction("Index", "Dashboard");
        }

        // Method to parse relative time
        private bool TryParseRelativeTime(string relativeTime, DateTime dueDate, out DateTime result)
        {
            result = default;

            if (string.IsNullOrEmpty(relativeTime))
                return false;

            try
            {
                var parts = relativeTime.Split(' ');
                if (parts.Length != 2)
                    return false;

                var value = int.Parse(parts[0]);
                var unit = parts[1].ToLower();

                switch (unit)
                {
                    case "days":
                        result = dueDate.AddDays(value);
                        break;
                    case "hours":
                        result = dueDate.AddHours(value);
                        break;
                    case "minutes":
                        result = dueDate.AddMinutes(value);
                        break;
                    default:
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
