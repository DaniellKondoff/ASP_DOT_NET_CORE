using System;
using System.Collections.Generic;
using System.Text;

namespace _2.OneToMany
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentCourse> Students { get; set; } = new List<StudentCourse>();
    }
}
