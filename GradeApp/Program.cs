using GradeApp.Models;
using System;

namespace GradeApp
{
    internal class Program
    {
        static void ConvertGradeMenu(String input)
        {
        
            {

                if (int.TryParse(input, out int grade))
                {
                    if (grade < 0 || grade > 100)
                    {
                        Console.WriteLine("Invalid grade. Please enter a grade between 0 and 100.");
                        return;
                    }
                    else
                    {
                        char letterGrade = grade switch
                        {
                            >= 90 and <= 100 => 'A',
                            >= 80 and < 90 => 'B',
                            >= 70 and < 80 => 'C',
                            >= 60 and < 70 => 'D',
                            _ => 'F'
                        };

                        Console.WriteLine($"Your letter grade is: {letterGrade}");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric grade.");
                }

            }

        }
        static void ShowMenu()
        {
            Console.WriteLine("\n \n \n ====================================");
            Console.WriteLine("            GradeApp Menu           ");
            Console.WriteLine("====================================");
            Console.WriteLine("1) Convert numeric grade to A-F");
            Console.WriteLine("2) Add new student ");
            Console.WriteLine("3) Search student (by ID or Name)");
            Console.WriteLine("4) Delete student (by ID)");
            Console.WriteLine("5) View all students");
            Console.WriteLine("0) Exit");
            Console.WriteLine("====================================");
        }

        static void Main(string[] args)
        {

       
            List<Student> Students = new List<Student>();


            while (true)
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter your numeric grade (0-100): ");
                        string gradeInput = Console.ReadLine();
                        ConvertGradeMenu(gradeInput);
                        break;
                    case "2":
                        Student newStudent = new Student();
                        Console.Write("Enter student name: ");
                        newStudent.Name = Console.ReadLine();
                        Console.Write("Enter student age: ");
                        newStudent.Age = int.Parse(Console.ReadLine());
                        Console.Write("Enter student date of birth (year): ");
                        newStudent.DateOfBirth = int.Parse(Console.ReadLine());
                        newStudent.StudentId = Students.Count > 0 ? Students.Max(s => s.StudentId) + 1 : 1; 
                        Students.Add(newStudent);
                        Console.WriteLine($"Student Details: Name: {newStudent.Name}, Age: {newStudent.Age}, Date of Birth: {newStudent.DateOfBirth}, Student ID: {newStudent.StudentId}");
                        break;

                    case "3":
                        Console.Write("Enter student Name to search: ");
                        string searchName = Console.ReadLine();
                        var foundStudents = Students.Where(s => s.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
                        if (foundStudents.Count > 0)
                        {
                            foreach (var student in foundStudents)
                            {
                                Console.WriteLine($"Found Student - ID: {student.StudentId}, Name: {student.Name}, Age: {student.Age}, Date of Birth: {student.DateOfBirth}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No student found with that name.");
                        }


                        break;
                    case "4":
                        Console.Write("Enter student ID or Name to delete: ");
                        string deleteInput = Console.ReadLine();
                        Student studentToDelete = null;
                        if (int.TryParse(deleteInput, out int deleteId))
                        {
                            studentToDelete = Students.FirstOrDefault(s => s.StudentId == deleteId);
                        }
                        else
                        {
                            studentToDelete = Students.FirstOrDefault(s => s.Name.Equals(deleteInput, StringComparison.OrdinalIgnoreCase));
                        }
                        break;
                    case "5":
                        if (Students.Count > 0)
                        {
                            Console.WriteLine("All Students:");
                            foreach (var student in Students)
                            {
                                Console.WriteLine($"ID: {student.StudentId}, Name: {student.Name}, Age: {student.Age}, Date of Birth: {student.DateOfBirth}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No students available.");
                        }

                        break;
                    case "0":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }


          


        }
     
    



    }
}

