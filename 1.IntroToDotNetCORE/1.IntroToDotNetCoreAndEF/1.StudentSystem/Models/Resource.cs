using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _1.StudentSystem.Models
{
    public class Resource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType TypeResource { get; set; }

        [Required]
        public string URL { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
