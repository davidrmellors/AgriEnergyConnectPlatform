using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;
using Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;

namespace Data
{
    public class AgriConnectContext : IdentityDbContext<User>
    {
        //static AgriConnectContext()
        //{
        //    string relativePath = @"..\..\Database"; 
        //    string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
        //    Debug.WriteLine($"DataDirectory Path: {absolutePath}"); 
        //    AppDomain.CurrentDomain.SetData("DataDirectory", absolutePath);
        //}

        public AgriConnectContext() : base("name=AgriConnectContext")
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<AgriConnectContext>());
        }

        public static AgriConnectContext Create()
        {
            return new AgriConnectContext();
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FundingSource> FundingSources { get; set;}
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // User has many posts
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithRequired(p => p.User)
                .HasForeignKey(p => p.UserID)
                .WillCascadeOnDelete(false);

            // Optionally disable cascading delete for Comments related directly to User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserID)
                .WillCascadeOnDelete(false); // Consider implications if enabled

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithRequired(r => r.User)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithMany(c => c.Users)
                .Map(m => {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("CourseID");
                    m.ToTable("UserCourses");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .Map(m => {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("ProjectID");
                    m.ToTable("UserProjects");
                });

            // Post Model
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostID);

            // Post has many Comments
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(c => c.PostID)
                .WillCascadeOnDelete(true); // Enable cascading delete here if needed

            // Comment Model
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentID);


            // Review Model
            modelBuilder.Entity<Review>()
                .HasKey(r => r.ReviewID);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID);

            modelBuilder.Entity<Review>()
                .HasRequired(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductID);


            // Transaction Model
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionID);

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserID);

            modelBuilder.Entity<Transaction>()
                .HasRequired(t => t.Product)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductID);


            // Course Model
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID);


            // Product Model
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithRequired(r => r.Product)
                .HasForeignKey(r => r.ProductID);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Transactions)
                .WithRequired(t => t.Product)
                .HasForeignKey(t => t.ProductID);


            // Project Model
            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectID);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.FundingSources)
                .WithMany(f => f.Projects)
                .Map(m => {
                    m.MapLeftKey("ProjectID");
                    m.MapRightKey("FundingSourceID");
                    m.ToTable("ProjectsFundingSources");
                });


            // FundingSource Model
            modelBuilder.Entity<FundingSource>()
                .HasKey(f => f.FundingSourceID);


        }

    }
}
