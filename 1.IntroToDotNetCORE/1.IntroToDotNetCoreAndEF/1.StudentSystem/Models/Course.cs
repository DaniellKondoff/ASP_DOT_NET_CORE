﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _1.StudentSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public ICollection<StudentCourse> Students { get; set; } = new List<StudentCourse>();

        public ICollection<Resource> Resources { get; set; } = new List<Resource>();

        public ICollection<Homework> HomeWorks { get; set; } = new List<Homework>();
    }
}
