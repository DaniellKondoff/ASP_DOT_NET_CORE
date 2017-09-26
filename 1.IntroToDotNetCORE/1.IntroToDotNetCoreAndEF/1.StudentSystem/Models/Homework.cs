using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _1.StudentSystem.Models
{
    public class Homework
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public ContentType TypeOfContent { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
