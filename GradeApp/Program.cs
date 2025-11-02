using GradeApp.Models;
using System;
using System.Globalization;

namespace GradeApp
{
    internal class Program
    {
        static (bool Success, char Letter, string ErrorMessage) ConvertGradeMenu(String input)
        {
        
            {

                if (int.TryParse(input, out int grade))
                {
                    if (grade < 0 || grade > 100)
                    {
                        return (false , '\0', "Invalid grade. Please enter a grade between 0 and 100.");
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

                        return (true, letterGrade, string.Empty);
                    }

                }
                else
                {
                    return (false, '\0', "Invalid input. Please enter a numeric grade.");
                }

            }

        }
        static bool TryReadDate(string label,string dateFormat,out DateTime date)
        {
            while (true)
            {
                Console.Write($"Enter {label} ({dateFormat}): ");
                string input = Console.ReadLine();

                bool ok = DateTime.TryParseExact(
                    input,
                    dateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out date
                );

                if (!ok)
                {
                    Console.WriteLine($"Invalid date. Please enter the date in format {dateFormat}.");
                    continue;
                }

                if (date > DateTime.Today)
                {
                    
                    Console.WriteLine("Date cannot be in the future. Please enter a past date (today or earlier).");
                    continue;
                }
                return true;

            }
        }
        static int ValidateAge()
        {
            int value;

            while (true)
            {
                Console.Write($"Enter : Student Age ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out value))
                {
                    Console.WriteLine("Invalid input. Please enter digits only (18, 25, 60 ...).");
                    continue;
                }
                if (value < 0 || value > 120)
                {
                    Console.WriteLine("Age out of range. Please enter an age between 0 and 120.");
                    continue;
                }

                return value;
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
                        var result = ConvertGradeMenu(gradeInput);
                        if (result.Success)
                        {
                            Console.WriteLine($"Your letter grade is: {result.Letter}");
                        }
                        else
                        {
                            Console.WriteLine(result.ErrorMessage);
                        }
                        break;
                    case "2":
                        Student newStudent = new Student();
                        Console.Write("Enter student name: ");
                        newStudent.Name = Console.ReadLine();
                      
                        newStudent.Age=ValidateAge();

                        if (TryReadDate("date of birth", "yyyy-MM-dd", out DateTime dob))
                        
                            newStudent.DateOfBirth = dob;
           
                        newStudent.StudentId = Students.Count > 0 ? Students.Max(s => s.StudentId) + 1 : 1; 
                        Students.Add(newStudent);
                        Console.WriteLine($"Student Details: Name: {newStudent.Name}, Age: {newStudent.Age}, Date of Birth: {newStudent.DateOfBirth.ToShortDateString()}, Student ID: {newStudent.StudentId}");
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

