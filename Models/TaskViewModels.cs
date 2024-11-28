using System;
using System.ComponentModel.DataAnnotations;

namespace NWU_Taskmaster.Models
{
    public class TaskViewModel
    {
        public int TaskId { get; set; } // For editing existing tasks

        [Display(Name = "Parent Task")]
        public int ParentTaskId { get; set; } // For linking subtasks to parent

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

    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
