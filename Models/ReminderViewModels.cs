using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace NWU_Taskmaster.Models
{
    public class ReminderViewModel
    {
        public int ReminderId { get; set; } // Used for editing or displaying reminders

        [Required]
        public int TaskId { get; set; } // The task the reminder is associated with

        [Required]
        [Display(Name = "Reminder Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReminderDate { get; set; } = DateTime.Now; // Exact date and time for the reminder

        [Display(Name = "Is Recurring?")]
        public bool IsRecurring { get; set; } = false; // Whether the reminder repeats

        [Display(Name = "Recurring Interval (e.g., '2 days', '1 week')")]
        public string RecurringInterval { get; set; } // Human-readable recurring interval

        [Display(Name = "Relative to Due Date?")]
        public bool RelativeToDueDate { get; set; } = false; // Whether this is a relative reminder

        [Display(Name = "Relative Time (e.g., '-1 days', '-3 hours')")]
        public string RelativeTime { get; set; } // Time offset for relative reminders

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } // Used for validation against task's due date
    }
}
