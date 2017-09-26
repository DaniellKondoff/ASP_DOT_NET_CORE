using _1.StudentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _1.StudentSystem.Data
{
    public class CourseConfiguration 
    {
        public CourseConfiguration(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.StudentId);

            builder
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            builder
                .HasMany(c => c.HomeWorks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);
        }
    }
}
