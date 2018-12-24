using P01_StudentSystem.Data;
using P01_StudentSystem.Data.Models;
using System;
using System.Linq;
using System.Text;

namespace P01_StudentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using (var context = new StudentSystemContext())
            {
                
                RestartDb(context);

                var student = context.Students
                    .Select(s => new
                    {
                        s.StudentId,
                        s.Name,
                        s.PhoneNumber,
                        s.RegisteredOn,
                        s.Birthday,
                        s.HomeworkSubmissions,
                        Courses = s.CourseEnrollments
                            .Select(c => new
                            {
                                c.Course,
                            }).ToArray()
                    }).ToArray();

                foreach (var x in student)
                {
                    Console.WriteLine($"Name: {x.Name} StudentId: {x.StudentId}");
                    Console.WriteLine();
                    Console.WriteLine($"PhoneNumber: {x.PhoneNumber}");
                    Console.WriteLine($"Birthday: {x.Birthday}");
                    Console.WriteLine($"RegisteredOn: {x.RegisteredOn}");
                    Console.WriteLine("--HomeworkSubmissions ");
                    foreach (var y in x.HomeworkSubmissions)
                    {
                        Console.WriteLine($"----Homework Content: {y.Content}");
                        Console.WriteLine($"----Homework Type: {y.ContentType}");
                        Console.WriteLine();
                    }
                    if (x.HomeworkSubmissions.Count() == 0)
                    {
                        Console.WriteLine("----no homework");
                    }

                    Console.WriteLine("--Courses ");
                    foreach (var y in x.Courses)
                    {
                        Console.WriteLine($"----Course Type: {y.Course.Name}");
                        Console.WriteLine($"----Course Content: {y.Course.Description}");
                        Console.WriteLine($"----Course Resources: ");
                        foreach (var z in y.Course.Resources)
                        {
                            Console.WriteLine($"------Resourcename: {z.Name}");
                            Console.WriteLine($"------Resourcetype: {z.ResourceType}");
                        }
                        Console.WriteLine($"----Coursehomework Content: ");
                        foreach (var v in y.Course.HomeworkSubmissions)
                        {
                            Console.WriteLine($"------Homework Content: {v.Content}");
                            Console.WriteLine($"------Homework Contenttype :{v.ContentType}");
                        }
                        Console.WriteLine($"----Course Startdate: {y.Course.StartDate}");
                        Console.WriteLine($"----Course Enddate: {y.Course.EndDate}");
                        Console.WriteLine($"------Course Price: {y.Course.Price}");
                    }
                    if (x.Courses.Count() == 0)
                    {
                        Console.WriteLine("----there is no enrollmented course");
                    }

                    Console.WriteLine("==========================================================");
                }
            }
        }

        private static void RestartDb(StudentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);
        }

        private static void Seed(StudentSystemContext context)
        {
            var students = new[]
            {
                new Student{ Name = "Петър Петров", RegisteredOn = new DateTime(2013,1,5), PhoneNumber = "0886055432", Birthday = new DateTime(1990,5,12),},
                new Student{ Name = "Георги Георгиев", RegisteredOn = new DateTime(2012,5,16), PhoneNumber = "0884055400", Birthday = new DateTime(1995,11,25),},
                new Student{ Name = "Иван Иванов", RegisteredOn = new DateTime(2010,1,5), PhoneNumber = "0888235678", Birthday = new DateTime(1990,1,9),},
            };
            context.Students.AddRange(students);

            var courses = new[]
            {
                 new Course{ Name = "руска литература", Description = "романи на руски език", StartDate =    new DateTime(2015,1,1), EndDate = new DateTime(2015,5,1), Price = 300 },
                 new Course{ Name = "физика", Description = "гравитация", StartDate = new DateTime(2015,1,2), EndDate = new DateTime(2015,5,5), Price = 400 },
                 new Course{ Name = "биология", Description = "анатомия", StartDate = new DateTime(2015,1,5), EndDate = new DateTime(2015,5,5), Price = 500 }
             };
             context.Courses.AddRange(courses);

             var recourses = new[]
             {
                 new Resource{ Name = "TV", Course = courses[0], ResourceType = ResourceType.Video},
                 new Resource{ Name = "Book" , Course = courses[1], ResourceType=ResourceType.Document},
                 new Resource{ Name = "Radio", Course = courses[2],ResourceType=ResourceType.Presentation}
             };
             context.Resources.AddRange(recourses);

             var homeworkCourse = new[]
             {
                 new Homework{ Content = "Ана Каренина - наизуст", ContentType = ContentType.Pdf,Course=  courses[0]},
                 new Homework{ Content = "Ана Каренина - 1 глава- наизуст", ContentType=ContentType.Pdf,Student = students[0]},
                 new Homework{ Content = "Теория на относителността - доразвиване", ContentType = ContentType.Zip, Course = courses[1]},
                 new Homework{ Content = "Теория на относителността - наизустяване", ContentType =ContentType.Zip, Student = students[1]},
                 new Homework{ Content = "биохимичен баланс в мозъка на светулките", ContentType =ContentType.Application, Course = courses[2]},
                 new Homework{ Content = "биохимичен баланс в мозъка на светулките - изчисляване на наличието на фосфор", ContentType = ContentType.Application, Student = students[2]},
             };
             context.HomeworkSubmissions.AddRange(homeworkCourse);

            var studentCourses = new[]
            {
                new StudentCourse{ Student = students[0], Course = courses[0]},
                new StudentCourse{ Student = students[1], Course = courses[1]},
                new StudentCourse{ Student = students[2], Course = courses[2]}
            };
            context.StudentCourses.AddRange(studentCourses);

            context.SaveChanges();
        }
    }
}
