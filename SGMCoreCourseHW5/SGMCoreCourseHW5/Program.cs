using System;
using System.Configuration;
using System.Data.SqlClient;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreCourseHW5
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StudyManager"].ConnectionString;
            var coursesRepository = new CoursesRepository(connectionString);
            var groupsRepository = new GroupsRepository(connectionString);
            var studentsRepository = new StudentsRepository(connectionString);
            var groupCoursesRepository = new GroupCoursesRepository(connectionString);
            try
            {
                Console.WriteLine($"--------Create");
                var course = coursesRepository.Create(new Course()
                {
                    LectorName = "Ivan Petrovych",
                    Name = "Mathematics"
                });

                Console.WriteLine($"--------Update");
                course.LectorName = "Stepan Ivanovich";
                coursesRepository.Update(course);

                Console.WriteLine($"--------GetSingle");
                var cw = coursesRepository.GetSingle(course.Id);
                Console.WriteLine($"{cw.Id}: {cw.Name}, {cw.LectorName}");

                Console.WriteLine($"--------Delete");
                coursesRepository.Delete(course.Id);

                Console.WriteLine($"--------GetAll");
                var courses = coursesRepository.GetAll();
                foreach (var cr in courses)
                {
                    Console.WriteLine($"{cr.Id}: {cr.Name}, {cr.LectorName}");
                }

                ////
                var course1 = coursesRepository.Create(new Course()
                {
                    LectorName = "Ivan Petrovych",
                    Name = "Mathematics"
                });

                var group1 = groupsRepository.Create(new Group()
                {
                    Name = "Mathematics",
                    Year = 2020
                });

                groupCoursesRepository.Create(new GroupCourse()
                {
                    CourseId = course1.Id,
                    GroupId = group1.Id
                });

                var student1 = studentsRepository.Create(new Student()
                {
                    GroupId = group1.Id,
                    AverageMark = 5,
                    BirthYear = 1999,
                    FirstName = "Petro",
                    LastName = "Semenuk"
                });

                Console.WriteLine($"--------StudentGroup");
                var studentsGroup = groupsRepository.GetStudentGroup(student1.Id);
               
                Console.WriteLine($"{student1.Id}: {studentsGroup.Name}");

                Console.WriteLine($"--------StudentCourses");
                var studentCourses = coursesRepository.GetStudentCourses(student1.Id);
                foreach (var cr in studentCourses)
                {
                    Console.WriteLine($"{cr.Id}: {cr.Name}, {cr.LectorName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
