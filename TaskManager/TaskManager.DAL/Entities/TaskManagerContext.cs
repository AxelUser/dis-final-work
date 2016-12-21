namespace TaskManager.DAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
            : base("name=TaskManagerContext")
        {
            //Database.SetInitializer(new TaskManagerDbInitializer());
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<TaskStatusType> TaskStatusTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ExecutorRole> ExecutorRoles { get; set; }
        public DbSet<ExecutorRoleType> ExecutorRoleTypes { get; set; }
    }
}