using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual int UserGroupId { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
