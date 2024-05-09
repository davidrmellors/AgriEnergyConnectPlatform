namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseTitle = c.String(),
                        CourseDescription = c.String(),
                        ContentURL = c.String(),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProjectID);
            
            CreateTable(
                "dbo.FundingSources",
                c => new
                    {
                        FundingSourceID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SourceName = c.String(),
                    })
                .PrimaryKey(t => t.FundingSourceID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        ProductID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductDescription = c.String(),
                        Category = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TimeStamp = c.DateTime(nullable: false),
                        ProductID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.CourseID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.ProjectsFundingSources",
                c => new
                    {
                        ProjectID = c.Int(nullable: false),
                        FundingSourceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectID, t.FundingSourceID })
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.FundingSources", t => t.FundingSourceID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.FundingSourceID);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ProjectID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserID", "dbo.Users");
            DropForeignKey("dbo.Transactions", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Transactions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "ProductID", "dbo.Products");
            DropForeignKey("dbo.UserProjects", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.UserProjects", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProjectsFundingSources", "FundingSourceID", "dbo.FundingSources");
            DropForeignKey("dbo.ProjectsFundingSources", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.UserCourses", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.UserProjects", new[] { "ProjectID" });
            DropIndex("dbo.UserProjects", new[] { "UserID" });
            DropIndex("dbo.ProjectsFundingSources", new[] { "FundingSourceID" });
            DropIndex("dbo.ProjectsFundingSources", new[] { "ProjectID" });
            DropIndex("dbo.UserCourses", new[] { "CourseID" });
            DropIndex("dbo.UserCourses", new[] { "UserID" });
            DropIndex("dbo.Transactions", new[] { "UserID" });
            DropIndex("dbo.Transactions", new[] { "ProductID" });
            DropIndex("dbo.Reviews", new[] { "UserID" });
            DropIndex("dbo.Reviews", new[] { "ProductID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropTable("dbo.UserProjects");
            DropTable("dbo.ProjectsFundingSources");
            DropTable("dbo.UserCourses");
            DropTable("dbo.Transactions");
            DropTable("dbo.Products");
            DropTable("dbo.Reviews");
            DropTable("dbo.FundingSources");
            DropTable("dbo.Projects");
            DropTable("dbo.Courses");
            DropTable("dbo.Users");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
