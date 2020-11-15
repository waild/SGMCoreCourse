using System;
using System.Collections.Generic;

namespace GenericList
{
    class Program
    {
        static void Main(string[] args)
        {
            var students = new SimpleList<Student>()
            {
                new Student {Age = 10, Course = "a1", Name = "Masha"},
                new Student {Age = 11, Course = "s3", Name = "Misha"}
            };

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------Add");
            var student1 = new Student {Age = 12, Course = "a1", Name = "Anna"};
            students.Add(student1);

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }
            Console.WriteLine("-----------Remove");
            students.Remove(student1);

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------RemoveAt");
            students.RemoveAt(0);

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------RemoveAll");
            students.RemoveAll();
            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------Add");
            students.Add(new Student { Age = 13, Course = "a4", Name = "Den" });
            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------Insert");
            students.Insert(0, student1);
            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine("-----------IndexOf");
            var index = students.IndexOf(student1);
            Console.WriteLine(index);

            Console.WriteLine("-----------Find");
            var item = students.Find(std => std.Name == "Den");
            Console.WriteLine(item.Name);

        }
    }

    class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public string Course { get; set; }
    }
}
