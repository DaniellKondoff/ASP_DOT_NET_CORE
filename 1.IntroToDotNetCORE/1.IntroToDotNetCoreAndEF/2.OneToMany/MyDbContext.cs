using _2.OneToMany;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


public class MyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=.;Database=MyTestDbCore;Integrated Security=True;");
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany(m => m.Slaves)
            .HasForeignKey(e => e.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentCourse>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentId);

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Students)
            .WithOne(sc => sc.Course)
            .HasForeignKey(sc => sc.CourseId);

        base.OnModelCreating(modelBuilder);
    }
}

