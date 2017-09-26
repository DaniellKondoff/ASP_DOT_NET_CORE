using _1.StudentSystem.Data;
using _1.StudentSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.StudentSystem.Client
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new StudentDb())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                //db.Database.Migrate();

                SeedData(db);
                //PrintStudentsHomeWorkSubmissions(db);
                //PrintAllCOursesWithResources(db);
                //PrintAllCoursesWithMoreThan5Resources(db);
                ////TODO
                //PrintStudentsCalculation(db);

                //3 Task

                //PrintAllCoursesWithResourceAndLicenses(db);
                db.SaveChanges();
            }
        }

        private static void SeedData(StudentDb db)
        {
            const int totalStudents = 25;
            const int totalCourses = 10;
            var currentDate = DateTime.Now;

            //Students
            Console.WriteLine("Adding Students");

            for (int i = 0; i < totalStudents; i++)
            {
                db.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(i),
                    BirthDate = currentDate.AddYears(-20).AddDays(i),
                    PhoneNumber = $"Random Phone {i}"
                });
            }
            db.SaveChanges();

            //Courses
            var addedCOurses = new List<Course>();

            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course Details {i}",
                    Price = 100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(20 + i)
                };

                db.Courses.Add(course);

                addedCOurses.Add(course);
            }
            db.SaveChanges();

            var random = new Random();
            //Students In Courses
            var studentIds = db.Students
                        .Select(s => s.Id)
                        .ToList();


            for (int i = 0; i < totalCourses; i++)
            {
                var studentsInCourse = random.Next(2, totalStudents / 2);
                var currentCourse = addedCOurses[i];

                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];

                    if (!currentCourse.Students.Any(s=>s.StudentId == studentId))
                    {
                        currentCourse.Students.Add(new StudentCourse
                        {
                            StudentId = studentId
                        });
                    }
                    else
                    {
                        j--;
                    }
                }

                var resourcesInCourse = random.Next(2, 20);
                var types = new[] { 0, 1, 2, 999 };

                currentCourse.Resources.Add(new Resource
                {
                    Name = $"Resource {i}",
                    URL = $"URL {i}",
                    TypeResource = (ResourceType)types[random.Next(0, types.Length)]
                });
            }

            db.SaveChanges();

            //HomeWorks
            for (int i = 0; i < totalCourses; i++)
            {
                var currentCourse = addedCOurses[i];
                var studentInCourseIds = currentCourse.Students.Select(s => s.StudentId).ToList();

                for (int j = 0; j < studentInCourseIds.Count; j++)
                {
                    var totalHomeWork = random.Next(2, 5);
                    for (int k = 0; k < totalHomeWork; k++)
                    {
                        db.HomeWork.Add(new Homework
                        {
                            Content = $"Content Homework {i}",
                            SubmissionDate = currentDate.AddDays(-i),
                            TypeOfContent = ContentType.zip,
                            StudentId = studentInCourseIds[j],
                            CourseId = currentCourse.Id
                        });
                    }
                }
            }
            db.SaveChanges();
        }

        private static void PrintAllCoursesWithResourceAndLicenses(StudentDb db)
        {
            var courses = db.Courses
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources
                    .Select(r => new
                    {
                        r.Name,
                        r.Licenses
                    })
                    .OrderByDescending(r => r.Licenses.Count)
                    .ThenBy(r => r.Name),
                    ResourcesCount = c.Resources.Count
                })
                .OrderByDescending(c => c.ResourcesCount)
                .ThenBy(c => c.Name);

            
        }

        private static void PrintStudentsCalculation(StudentDb db)
        {
            var students = db
                .Students
                .Where(s=>s.Courses.Any())
                .Select(s => new
                {
                    s.Name,
                    NumberOfCources = s.Courses.Count,
                    TotalPrice = s.Courses.DefaultIfEmpty().Sum(c => c.Course.Price),
                    AveragePrice = s.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCources)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name} - {student.NumberOfCources} - {student.TotalPrice} - {student.AveragePrice}");
            }
        }

        private static void PrintAllCoursesWithMoreThan5Resources(StudentDb db)
        {
            var courses = db.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c=>c.Resources.Count)
                .ThenBy(c=>c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    ResourceCount = c.Resources.Count
                });

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} - {course.ResourceCount}");
            }
        }

        private static void PrintAllCOursesWithResources(StudentDb db)
        {
            var courseData = db.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        r.TypeResource,
                        r.URL
                    })
                })
                .ToList();

            foreach (var course in courseData)
            {
                Console.WriteLine($"{course.Name} - {course.Description}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"  {resource.Name}");
                    Console.WriteLine($"  {resource.TypeResource}");
                    Console.WriteLine($"  {resource.URL}");
                }
            }

        }

        private static void PrintStudentsHomeWorkSubmissions(StudentDb db)
        {
            var studentsData = db.Students
                .Select(s => new
                {
                    s.Name,
                    Content = s.HomeWorks.Select(h => new {
                        h.Content,
                        h.TypeOfContent
                    })
                })
                .ToList();

            foreach (var student in studentsData)
            {
                Console.WriteLine($"Student: {student.Name}");
                foreach (var content in student.Content)
                {
                    Console.WriteLine($"Content: {content.Content}");
                    Console.WriteLine($"ContentType {content.TypeOfContent}");
                }
            }

        }
    }
}
