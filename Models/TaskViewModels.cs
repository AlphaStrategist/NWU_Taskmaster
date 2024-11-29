using System;
using System.ComponentModel.DataAnnotations;

namespace NWU_Taskmaster.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; } 

        [Display(Name = "Parent Task")]
        public int ParentTaskId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [StringLength(1000)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
    }
}
