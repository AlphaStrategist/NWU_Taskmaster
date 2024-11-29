using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace NWU_Taskmaster.Models
{
    public class ReminderViewModel
    {
        public int ReminderId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Reminder Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReminderDate { get; set; } = DateTime.Now;

        [Display(Name = "Is Recurring?")]
        public bool IsRecurring { get; set; } = false;

        [Display(Name = "Recurring Interval (e.g., '2 days', '1 week')")]
        public string RecurringInterval { get; set; } 

        [Display(Name = "Relative to Due Date?")]
        public bool RelativeToDueDate { get; set; } = false; 

        [Display(Name = "Relative Time (e.g., '-1 days', '-3 hours')")]
        public string RelativeTime { get; set; } 

        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
    }
}
