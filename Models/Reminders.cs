using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NWU_Taskmaster.Models
{
    public class Reminders
    {
        [Key]
        public int reminder_id { get; set; }

        public int task_id { get; set; }
        public DateTime? reminder_date { get; set; }
        public bool is_recurring { get; set; } = false;
        public string recurring_interval { get; set; }
        public bool relative_to_due_date { get; set; } = false;
        public string relative_time { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public bool is_triggered { get; set; } = false;
    }
}