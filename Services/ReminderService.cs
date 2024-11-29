using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NWU_Taskmaster.Models;

namespace NWU_Taskmaster.Services
{
    public class ReminderService
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public void CheckAndNotifyReminders()
        {
            // Get current time
            var now = DateTime.Now;
            // Get the 5-minute threshold
            var buffertime = now.AddMinutes(5);

            // Fetch reminders that are within the next 5 minutes and not yet triggered
            var upcomingReminders = db.Reminders
                .Where(r => r.reminder_date >= now
                            && r.reminder_date <= buffertime
                            && !r.is_triggered)
                .ToList();

            foreach (var reminder in upcomingReminders)
            {
                MarkReminderAsNotified(reminder);
            }
        }

        private void MarkReminderAsNotified(Reminders reminder)
        {
            reminder.is_triggered = true;
            db.SaveChanges();
        }
    }
}
