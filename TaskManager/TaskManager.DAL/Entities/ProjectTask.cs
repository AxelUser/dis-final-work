using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int EstimatedDifficult { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        public int? TaskStatusTypeId { get; set; }
        public virtual TaskStatusType TaskStatusType { get; set; }
        public virtual IEnumerable<ExecutorRole> ExecutorRoles { get; set; }
    }
}
