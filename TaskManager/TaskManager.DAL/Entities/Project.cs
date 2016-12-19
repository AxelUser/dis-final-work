using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<ProjectTask> Tasks { get; set; }
    }
}
