using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entities
{
    public class TaskManagerDbInitializer: DropCreateDatabaseIfModelChanges<TaskManagerContext>
    {
        protected override void Seed(TaskManagerContext context)
        {
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

            context.UserGroups.AddRange(roles);
            base.Seed(context);
        }
    }
}
