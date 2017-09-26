using _1.StudentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _1.StudentSystem.Data
{
    public class StudentDb : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeWork { get; set; }
        public DbSet<License> Liceses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=StudentSystemCore;Integrated Security=True;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId);

            new CourseConfiguration(builder.Entity<Course>());
 
            builder.Entity<Homework>()
                .HasOne(h => h.Student)
                .WithMany(s => s.HomeWorks)
                .HasForeignKey(h => h.StudentId);

            builder.Entity<Resource>()
                .HasMany(r => r.Licenses)
                .WithOne(l => l.Resource)
                .HasForeignKey(l => l.ResourceId);

            
            base.OnModelCreating(builder);
        }
    }
}
