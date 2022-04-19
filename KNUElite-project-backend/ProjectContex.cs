using KNUElite_project_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = KNUElite_project_backend.Models.Task;
using Type = KNUElite_project_backend.Models.Type;

namespace KNUElite_project_backend
{
    public class ProjectContex : DbContext //, IProjectContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<UserMeeting> UserMeetings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Type> Types { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<File> Files { get; set; }
        public ProjectContex(DbContextOptions<ProjectContex> options) : base(options)
        {
        }
        
        public System.Threading.Tasks.Task Save()
        {
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<UserProject>().ToTable("UserProjects");
            modelBuilder.Entity<Meeting>().ToTable("Meetings");
            modelBuilder.Entity<UserMeeting>().ToTable("UserMeetings");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Type>().ToTable("Types");
            modelBuilder.Entity<Task>().ToTable("Tasks");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<File>().ToTable("Files");

            // Configure Primary Keys  
            modelBuilder.Entity<Role>().HasKey(r => r.Id).HasName("PK_Roles");
            modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Users");
            modelBuilder.Entity<Project>().HasKey(p => p.Id).HasName("PK_Projects");
            modelBuilder.Entity<UserProject>().HasKey(up => up.Id).HasName("PK_UserProjects");
            modelBuilder.Entity<Meeting>().HasKey(m=> m.Id).HasName("PK_Meetings");
            modelBuilder.Entity<UserMeeting>().HasKey(um => um.Id).HasName("PK_UserMeetings");
            modelBuilder.Entity<Status>().HasKey(s => s.Id).HasName("PK_Statuses");
            modelBuilder.Entity<Type>().HasKey(t => t.Id).HasName("PK_Types");
            modelBuilder.Entity<Task>().HasKey(t => t.Id).HasName("PK_Tasks");
            modelBuilder.Entity<Comment>().HasKey(t => t.Id).HasName("PK_Comments");
            modelBuilder.Entity<File>().HasKey(t => t.Id).HasName("PK_Files");

            //configure indexes
            modelBuilder.Entity<Role>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Status>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Type>().HasIndex(p => p.Name).IsUnique();

            // Configure columns  
            modelBuilder.Entity<Role>().Property(r => r.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Role>().Property(r => r.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<User>().Property(u => u.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.RoleId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Project>().Property(p => p.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.Name).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.Description).HasColumnType("text");

            modelBuilder.Entity<UserProject>().Property(up => up.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<UserProject>().Property(up => up.ProjectId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<UserProject>().Property(up => up.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Meeting>().Property(m => m.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Meeting>().Property(m => m.Title).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<Meeting>().Property(m => m.Time).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Meeting>().Property(m => m.ProjectId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<UserMeeting>().Property(um => um.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<UserMeeting>().Property(um => um.MeetingId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<UserMeeting>().Property(um => um.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Status>().Property(s => s.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Status>().Property(r => r.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<Type>().Property(t => t.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Type>().Property(t => t.Name).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<Task>().Property(t => t.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.Title).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.Description).HasColumnType("text");
            modelBuilder.Entity<Task>().Property(t => t.LoggedTime).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.EstimatedTime).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.ProjectId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.TypeId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.StatusId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Task>().Property(t => t.AssigneeId).HasColumnType("int");
            modelBuilder.Entity<Task>().Property(t => t.ReporterId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<Comment>().Property(c => c.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.CommentText).HasColumnType("text").IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.Time).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.TaskId).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.UserId).HasColumnType("int").IsRequired();

            modelBuilder.Entity<File>().Property(f => f.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<File>().Property(f => f.RootDirectory).HasColumnType("nvarchar(255)").IsRequired();
            modelBuilder.Entity<File>().Property(f => f.FileName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<File>().Property(f => f.CreationDate).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<File>().Property(f => f.FileSize).HasColumnType("bigint").IsRequired();
            modelBuilder.Entity<File>().Property(f => f.FileStream).HasColumnType("text").IsRequired();
            modelBuilder.Entity<File>().Property(f => f.TaskId).HasColumnType("int").IsRequired();

            // Configure relationships  
            modelBuilder.Entity<User>().HasOne(u=>u.Role).WithMany(r=>r.Users).HasPrincipalKey(r => r.Id).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Users_Roles");
            modelBuilder.Entity<UserProject>().HasOne(up => up.User).WithMany(u => u.UserProjects).HasPrincipalKey(r => r.Id).HasForeignKey(up => up.UserId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Users_UserProjects");
            modelBuilder.Entity<UserProject>().HasOne(up => up.Project).WithMany(u => u.UserProjects).HasPrincipalKey(r => r.Id).HasForeignKey(up => up.ProjectId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Projects_UserProjects");
            modelBuilder.Entity<Meeting>().HasOne(m => m.Project).WithMany(p => p.Meetings).HasPrincipalKey(m => m.Id).HasForeignKey(m => m.ProjectId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Meetings_Projects");
            modelBuilder.Entity<UserMeeting>().HasOne(um => um.Meeting).WithMany(m => m.UserMeetings).HasPrincipalKey(m => m.Id).HasForeignKey(um => um.MeetingId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Meetings_UserMeetings");
            modelBuilder.Entity<UserMeeting>().HasOne(um => um.User).WithMany(u => u.UserMeetings).HasPrincipalKey(u => u.Id).HasForeignKey(um => um.UserId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Users_UserMeetings");
            modelBuilder.Entity<Task>().HasOne(t => t.Project).WithMany(p => p.Tasks).HasPrincipalKey(p => p.Id).HasForeignKey(t => t.ProjectId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Tasks_Projects");
            modelBuilder.Entity<Task>().HasOne(t => t.Type).WithMany(t => t.Tasks).HasPrincipalKey(t => t.Id).HasForeignKey(t => t.TypeId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Tasks_Types");
            modelBuilder.Entity<Task>().HasOne(t => t.Status).WithMany(s => s.Tasks).HasPrincipalKey(s => s.Id).HasForeignKey(t => t.StatusId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Tasks_Statuses");
            modelBuilder.Entity<Task>().HasOne(t => t.Assignee).WithMany(a => a.AssigneeTasks).HasPrincipalKey(a => a.Id).HasForeignKey(t => t.AssigneeId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Tasks_Assignees");
            modelBuilder.Entity<Task>().HasOne(t => t.Reporter).WithMany(r => r.ReporterTasks).HasPrincipalKey(r => r.Id).HasForeignKey(t => t.ReporterId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Tasks_Reporters");
            modelBuilder.Entity<Comment>().HasOne(c => c.Task).WithMany(t => t.Comments).HasPrincipalKey(t => t.Id).HasForeignKey(c => c.TaskId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Comments_Tasks");
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasPrincipalKey(u => u.Id).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Comments_Users");
            modelBuilder.Entity<File>().HasOne(f => f.Task).WithMany(t => t.Files).HasPrincipalKey(t => t.Id).HasForeignKey(f => f.TaskId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Files_Tasks");

        }
    }
}

