using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NWU_Taskmaster.Models
{
    public class ClientTasks
    {
        [Key]
        public int task_id { get; set; }

        public int client_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime due_date { get; set; }
        public DateTime created_at { get; set; }
        public bool is_completed { get; set; }
        public int? parent_task_id { get; set; }
        public int? category_id { get; set; }
        public int priority { get; set; }
    }
}