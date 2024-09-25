using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagament.Models
{
    public partial class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }
        public virtual DbSet<TraineeCourse> TraineeCourses { get; set; }
        public virtual DbSet<Trainer> Trainers { get; set; }
        public virtual DbSet<Units> Units { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=AHMAD-KHALID\\SQLEXPRESS;Initial Catalog=EductionalCentre;Integrated Security=SSPI;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Category_Category");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CategoryId).HasColumnName("category_Id");
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.Descripition).HasMaxLength(200);
                entity.Property(e => e.ImageId).HasMaxLength(200).HasColumnName("Image_id");
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.TrainerId).HasColumnName("Trainer_Id");

                entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Category");

                entity.HasOne(d => d.Trainer).WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Course_Trainer");
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.UnitId).HasColumnName("UnitId");
                entity.Property(e => e.OrderNumber).HasColumnName("Order_Number");
                entity.Property(e => e.Title).HasMaxLength(200);
                entity.Property(e => e.Type).HasMaxLength(200);


                entity.HasOne(d => d.Units).WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lessons_Course");
            });

            modelBuilder.Entity<Trainee>(entity =>
            {
                entity.ToTable("Trainee");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreationDate).HasColumnType("datetime").HasColumnName("Creation_Date");
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasColumnName("Is_Active");
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(200);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Trainee)
                    .HasForeignKey<ApplicationUser>(d => d.TraineeId);
            });

            modelBuilder.Entity<TraineeCourse>(entity =>
            {
                entity.HasKey(e => new { e.TraineeId, e.CourseId });

                entity.ToTable("Trainee_courses");

                entity.Property(e => e.TraineeId).HasColumnName("Trainee_Id");
                entity.Property(e => e.CourseId).HasColumnName("Course_Id");
                entity.Property(e => e.RegistrationDate).HasColumnType("datetime").HasColumnName("Registration_Date");

                entity.HasOne(d => d.Course).WithMany(p => p.TraineeCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Trainee_courses_Course");

                entity.HasOne(d => d.Trainee).WithMany(p => p.TraineeCourses)
                    .HasForeignKey(d => d.TraineeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Trainee_courses_Trainee");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("Trainer");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CreationDate).HasColumnType("datetime").HasColumnName("Creation_Date");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.WebSite).HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Trainer)
                    .HasForeignKey<ApplicationUser>(d => d.TrainerId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
