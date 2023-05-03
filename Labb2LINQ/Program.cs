using Labb2LINQ.Models;
using Labb2LINQ.Data;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace Labb2LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Hämta alla lärare som undervisar matte");
                Console.WriteLine("2. Hämta alla elever med sina lärare.");
                Console.WriteLine("3. Kolla om ämnen tabell Contains programmering1 eller inte.");
                Console.WriteLine("4. Editera en Ämne från programmering2 till OOP");
                Console.WriteLine("5. Uppdatera en student record om sin lärare är Anas till Reidar.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GetAllTeacherTeachingMath();
                        break;
                    case "2":
                        GetAllStudentWithTeachers();
                        break;
                    case "3":
                        SubjectContainsPrg1();
                        break;
                    case "4":
                        EditSubjectPrg2();
                        break;
                    case "5":
                        UpdateStudentsTeacher();
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again");
                        break;
                }

                Console.WriteLine();
            }
        }
        public static void AddAllData()
        {
            var context = new LINQDBContext();

            // Teachers
            Teacher john = new Teacher { FirstName = "John", LastName = "Doe", SocialSecurityNumber = "111" };
            Teacher jane = new Teacher { FirstName = "Jane", LastName = "Smith", SocialSecurityNumber = "112" };
            Teacher bert = new Teacher { FirstName = "Bert", LastName = "Fjart", SocialSecurityNumber = "113" };
            context.Teachers.AddRange(john, jane, bert);

            // Subjects
            Subject math = new Subject { Name = "Math" };
            Subject english = new Subject { Name = "English" };
            Subject history = new Subject { Name = "History" };
            Subject programmering2 = new Subject { Name = "Programmering2" };
            context.Subjects.AddRange(math, english, history);

            // Courses
            Course sut22 = new Course { Name = "SUT22" };
            Course sut23 = new Course { Name = "SUT23" };
            context.Course.AddRange(sut22, sut23);

            // Students 
            var bob = new Student { FirstName = "Bob", LastName = "Bobman", Teacher = john };
            var alice = new Student { FirstName = "Alice", LastName = "Alicewoman", Teacher = jane };
            var charlie = new Student { FirstName = "Charlie", LastName = "Charlieman", Teacher = john };
            context.Students.AddRange(bob, alice, charlie);
        }
        public static void AddSubjectsToTeachers()
        {
            var context = new LINQDBContext();

            var john = context.Teachers.FirstOrDefault(t => t.FirstName == "John");

            var jane = context.Teachers.FirstOrDefault(t => t.FirstName == "Jane");
            
            var bert = context.Teachers.FirstOrDefault(t => t.FirstName == "Bert");


            john.Subjects = new List<Subject>
            {
                context.Subjects.FirstOrDefault(t => t.Name == "math"),
                context.Subjects.FirstOrDefault(t => t.Name == "english")

            };

            jane.Subjects = new List<Subject>
            {
                context.Subjects.FirstOrDefault(t => t.Name == "history")
            };

            bert.Subjects = new List<Subject>
            {
                context.Subjects.FirstOrDefault(t => t.Name == "Programmering2")
            };

            context.SaveChanges();
        }
        public static void AddSubjectsToCourses()
        {
            var context = new LINQDBContext();

            var sut22 = context.Course.FirstOrDefault(t => t.Name == "sut22");

            var sut23 = context.Course.FirstOrDefault(t => t.Name == "sut23");

            sut22.Subjects = new List<Subject>
            {
                context.Subjects.FirstOrDefault(t => t.Name == "math"),
            };

            sut23.Subjects = new List<Subject>
            {
                context.Subjects.FirstOrDefault(t => t.Name == "history")
            };

            context.SaveChanges();
        }
        public static void GetAllTeacherTeachingMath()
        {
            var context = new LINQDBContext();

            var mathTeachers = context.Teachers
                .Where(t => t.Subjects.Any(s => s.Name == "Math"))
                .ToList();

            foreach (var teacher in mathTeachers)
            {
                Console.WriteLine($"Teacher {teacher.FirstName} {teacher.LastName} teaches Math");
            }
        }
        public static void GetAllStudentWithTeachers()
        {
            var context = new LINQDBContext();

            var studentsWithTeachers = context.Students.Include(s => s.Teacher);
            foreach (var teacher in studentsWithTeachers)
            {
                Console.WriteLine($"Student {teacher.FirstName} {teacher.LastName} With Teacher {teacher.Teacher.FirstName} {teacher.Teacher.LastName}");
            }
        }
        public static void SubjectContainsPrg1()
        {
            var context = new LINQDBContext();

            bool containsProgramming1 = context.Subjects.Any(s => s.Name == "programmering1");

            if (containsProgramming1)
            {
                Console.WriteLine("Ämnen tabell innehåller programmering1");
            }
            else
            {
                Console.WriteLine("Ämnen tabell innehåller inte programmering1");
            }
        }
        public static void EditSubjectPrg2()
        {
            var context = new LINQDBContext();

            // Get the subject to edit
            var subjectToEdit = context.Subjects.FirstOrDefault(s => s.Name == "Programmering2");

            if (subjectToEdit != null)
            {
                // Edit the name
                subjectToEdit.Name = "OOP";

                Console.WriteLine("Subject Programmering2 has been edited to OOP");
                // Save changes to database
                context.SaveChanges();
            }
        }
        public static void UpdateStudentsTeacher()
        {
            var context = new LINQDBContext();

            var bob = context.Students.FirstOrDefault(t => t.FirstName == "Bob");

            if (bob != null)
            {
                bob.TeacherId = 4;

                context.SaveChanges();
            }
        }
    }
}