namespace TaskManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExecutorRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExecutorRoleTypeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExecutorRoleTypes", t => t.ExecutorRoleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExecutorRoleTypeId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.ExecutorRoleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Caption = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        EstimatedDifficult = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        TaskStatusTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskStatusTypes", t => t.TaskStatusTypeId)
                .Index(t => t.TaskStatusTypeId);
            
            CreateTable(
                "dbo.TaskStatusTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasswordHash = c.String(nullable: false),
                        FullName = c.String(),
                        Email = c.String(nullable: false),
                        UserGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserGroupId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Caption = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.ExecutorRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.ExecutorRoles", "TaskId", "dbo.ProjectTasks");
            DropForeignKey("dbo.ProjectTasks", "TaskStatusTypeId", "dbo.TaskStatusTypes");
            DropForeignKey("dbo.ExecutorRoles", "ExecutorRoleTypeId", "dbo.ExecutorRoleTypes");
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserGroupId" });
            DropIndex("dbo.ProjectTasks", new[] { "TaskStatusTypeId" });
            DropIndex("dbo.ExecutorRoles", new[] { "TaskId" });
            DropIndex("dbo.ExecutorRoles", new[] { "ExecutorRoleTypeId" });
            DropIndex("dbo.ExecutorRoles", new[] { "UserId" });
            DropTable("dbo.Projects");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Users");
            DropTable("dbo.TaskStatusTypes");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.ExecutorRoleTypes");
            DropTable("dbo.ExecutorRoles");
        }
    }
}
