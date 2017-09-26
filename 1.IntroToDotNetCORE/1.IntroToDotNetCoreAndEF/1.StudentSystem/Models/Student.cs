using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _1.StudentSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public ICollection<Homework> HomeWorks { get; set; } = new List<Homework>();

    }
}
