using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NWU_Taskmaster.Models;

namespace NWU_Taskmaster.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Task/Index
        public ActionResult Index(int? parentTaskId = null)
        {
            var model = new TaskViewModel
            {
                ParentTaskId = parentTaskId ?? 0,
                TaskId = 0, 
                DueDate = DateTime.Now 
            };

            ViewBag.Categories = GetCategoriesForDropdown();
            return View(model);
        }

        // POST: /Task/SaveTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTask(TaskViewModel model, string DueTime)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategoriesForDropdown();
                return View("Index", model);
            }

            // Combine DueDate and DueTime
            if (!DateTime.TryParse($"{model.DueDate:yyyy-MM-dd} {DueTime}", out DateTime fullDueDate))
            {
                ModelState.AddModelError("DueDate", "Invalid date or time format.");
                ViewBag.Categories = GetCategoriesForDropdown();
                return View("Index", model);
            }

            if (fullDueDate < DateTime.Now)
            {
                ModelState.AddModelError("DueDate", "The due date cannot be in the past.");
                ViewBag.Categories = GetCategoriesForDropdown();
                return View("Index", model);
            }

            model.DueDate = fullDueDate;

            if (model.TaskId == 0)
            {
                var userId = User.Identity.GetUserId();
                var clientId = db.Clients.FirstOrDefault(c => c.aspnet_user_id == userId);
                
                // Create a new task
                var task = new ClientTasks
                {
                    client_id = clientId.client_id,
                    title = model.Title,
                    description = model.Description,
                    due_date = model.DueDate,
                    created_at = DateTime.Now,
                    is_completed = false,
                    parent_task_id = model.ParentTaskId,
                    category_id = model.CategoryId,
                    priority = model.Priority
                };

                db.ClientTasks.Add(task);
            }
            else
            {
                // Edit an existing task
                var existingTask = db.ClientTasks.FirstOrDefault(t => t.task_id == model.TaskId);
                if (existingTask == null) return HttpNotFound();

                existingTask.title = model.Title;
                existingTask.description = model.Description;
                existingTask.due_date = model.DueDate;
                existingTask.category_id = model.CategoryId;
                existingTask.priority = model.Priority;
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: /Task/EditTask
        public ActionResult EditTask(int id)
        {
            var task = db.ClientTasks.FirstOrDefault(t => t.task_id == id);
            if (task == null) return HttpNotFound();

            var model = new TaskViewModel
            {
                TaskId = task.task_id,
                ParentTaskId = task.parent_task_id ?? 0,
                Title = task.title,
                Description = task.description,
                DueDate = task.due_date,
                Priority = task.priority,
                CategoryId = task.category_id
            };

            ViewBag.Categories = GetCategoriesForDropdown(task.category_id);
            return View("Index", model);
        }

        // POST: /Task/DeleteTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTask(int id)
        {
            var task = db.ClientTasks.FirstOrDefault(t => t.task_id == id);

            if (task == null)
            {
                TempData["Error"] = "Task not found.";
                return RedirectToAction("Index", "Dashboard");
            }

            // Handle deletion of subtasks if applicable
            var childTasks = db.ClientTasks.Where(t => t.parent_task_id == id).ToList();
            db.ClientTasks.RemoveRange(childTasks);

            // Remove the task itself
            db.ClientTasks.Remove(task);
            db.SaveChanges();

            TempData["Success"] = "Task deleted successfully.";
            return RedirectToAction("Index", "Dashboard");
        }

        private SelectList GetCategoriesForDropdown(int? selectedCategoryId = null)
        {
            return new SelectList(db.TaskCategories, "category_id", "category_name", selectedCategoryId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
