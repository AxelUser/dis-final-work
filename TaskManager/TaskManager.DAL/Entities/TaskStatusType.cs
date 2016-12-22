using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class TaskStatusType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Caption { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
