using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using StudyManager.DataAccess.ADO;
using StudyManager.Models;

namespace SGMCoreCourseHW5
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDB();
            Console.ReadKey();
        }


        static async Task TestDB()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StudyManager"].ConnectionString;
            var coursesRepository = new CoursesRepository(connectionString);
            var groupsRepository = new GroupsRepository(connectionString);
            var studentsRepository = new StudentsRepository(connectionString);
            var groupCoursesRepository = new GroupCoursesRepository(connectionString);
            try
            {
                Console.WriteLine($"--------CreateAsync");
                var course = await coursesRepository.CreateAsync(new Course()
                {
                    LectorName = "Ivan Petrovych",
                    Name = "Mathematics"
                });

                Console.WriteLine($"--------UpdateAsync");
                course.LectorName = "Stepan Ivanovich";
                await coursesRepository.UpdateAsync(course);

                Console.WriteLine($"--------GetSingleAsync");
                var cw = await coursesRepository.GetSingleAsync(course.Id);
                Console.WriteLine($"{cw.Id}: {cw.Name}, {cw.LectorName}");

                Console.WriteLine($"--------DeleteAsync");
                await coursesRepository.DeleteAsync(course.Id);

                Console.WriteLine($"--------GetAllAsync");
                var courses = await coursesRepository.GetAllAsync();
                foreach (var cr in courses)
                {
                    Console.WriteLine($"{cr.Id}: {cr.Name}, {cr.LectorName}");
                }

                ////
                var course1 = await coursesRepository.CreateAsync(new Course()
                {
                    LectorName = "Ivan Petrovych",
                    Name = "Mathematics"
                });

                var group1 = await groupsRepository.CreateAsync(new Group()
                {
                    Name = "Mathematics",
                    Year = 2020
                });

                await groupCoursesRepository.CreateAsync(new GroupCourse()
                {
                    CourseId = course1.Id,
                    GroupId = group1.Id
                });

                var student1 = await studentsRepository.CreateAsync(new Student()
                {
                    GroupId = group1.Id,
                    AverageMark = 5,
                    BirthYear = 1999,
                    FirstName = "Petro",
                    LastName = "Semenuk"
                });

                Console.WriteLine($"--------StudentGroup");
                var studentsGroup = await groupsRepository.GetStudentGroup(student1.Id);
               
                Console.WriteLine($"{student1.Id}: {studentsGroup.Name}");

                Console.WriteLine($"--------StudentCourses");
                var studentCourses = await coursesRepository.GetStudentCourses(student1.Id);
                foreach (var cr in studentCourses)
                {
                    Console.WriteLine($"{cr.Id}: {cr.Name}, {cr.LectorName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
