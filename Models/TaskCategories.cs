using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NWU_Taskmaster.Models
{
    public class TaskCategories
    {
        [Key]
        public int category_id { get; set; }

        public string category_name { get; set; }
    }
}