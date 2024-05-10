namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustUserModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "UserID", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "UserID", "dbo.Users");
            DropForeignKey("dbo.Transactions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Reviews", new[] { "UserID" });
            DropIndex("dbo.Transactions", new[] { "UserID" });
            DropIndex("dbo.UserCourses", new[] { "UserID" });
            DropIndex("dbo.UserProjects", new[] { "UserID" });
            DropPrimaryKey("dbo.AspNetUsers");
            DropPrimaryKey("dbo.UserCourses");
            DropPrimaryKey("dbo.UserProjects");
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Comments", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Posts", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Reviews", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Transactions", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserCourses", "UserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserProjects", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            AddPrimaryKey("dbo.UserCourses", new[] { "UserID", "CourseID" });
            AddPrimaryKey("dbo.UserProjects", new[] { "UserID", "ProjectID" });
            CreateIndex("dbo.Comments", "UserID");
            CreateIndex("dbo.Posts", "UserID");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Reviews", "UserID");
            CreateIndex("dbo.Transactions", "UserID");
            CreateIndex("dbo.UserCourses", "UserID");
            CreateIndex("dbo.UserProjects", "UserID");
            AddForeignKey("dbo.Comments", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserCourses", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserProjects", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "UserID");
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Reviews", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProjects", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCourses", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserProjects", new[] { "UserID" });
            DropIndex("dbo.UserCourses", new[] { "UserID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "UserID" });
            DropIndex("dbo.Reviews", new[] { "UserID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropPrimaryKey("dbo.UserProjects");
            DropPrimaryKey("dbo.UserCourses");
            DropPrimaryKey("dbo.AspNetUsers");
            AlterColumn("dbo.UserProjects", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.UserCourses", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
            AlterColumn("dbo.Posts", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "Id");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddPrimaryKey("dbo.UserProjects", new[] { "UserID", "ProjectID" });
            AddPrimaryKey("dbo.UserCourses", new[] { "UserID", "CourseID" });
            AddPrimaryKey("dbo.AspNetUsers", "UserID");
            CreateIndex("dbo.UserProjects", "UserID");
            CreateIndex("dbo.UserCourses", "UserID");
            CreateIndex("dbo.Transactions", "UserID");
            CreateIndex("dbo.Reviews", "UserID");
            CreateIndex("dbo.Posts", "UserID");
            CreateIndex("dbo.Comments", "UserID");
            AddForeignKey("dbo.Reviews", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UserProjects", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.UserCourses", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "UserID", "dbo.Users", "UserID");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
        }
    }
}
