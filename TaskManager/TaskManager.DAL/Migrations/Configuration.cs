namespace TaskManager.DAL.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManager.DAL.Entities.TaskManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManager.DAL.Entities.TaskManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var roles = new UserGroup[]
            {
                new UserGroup()
                {
                    Id = 10,
                    Name = "Admin",
                    Caption = "Admin"
                },
                new UserGroup()
                {
                    Id = 20,
                    Name = "User",
                    Caption = "User"
                }
            };
            var types = new ExecutorRoleType[]
            {
                new ExecutorRoleType()
                {
                    Id = 1,
                    Name = "dev",
                    Caption = "Разработчик"
                },
                new ExecutorRoleType()
                {
                    Id = 2,
                    Name = "tester",
                    Caption = "Специалист по тестированию",
                }
            };
            var statuses = new TaskStatusType[]
            {
                new TaskStatusType()
                {
                    Id = 1,
                    Caption = "Отложен"
                },
                new TaskStatusType()
                {
                    Id = 2,
                    Caption = "В работе"
                },
                new TaskStatusType()
                {
                    Id = 3,
                    Caption = "Готово"
                }
            };
            context.UserGroups.AddOrUpdate(roles);
            context.ExecutorRoleTypes.AddOrUpdate(types);
            context.TaskStatusTypes.AddOrUpdate(statuses);
        }
    }
}
