using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class ExecutorRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExecutorRoleTypeId { get; set; }
        public int ProjectTaskId { get; set; }
        public virtual User User { get; set; }
        public virtual ExecutorRoleType ExecutorRoleType { get; set; }
        public virtual ProjectTask ProjectTasks { get; set; }
    }
}
