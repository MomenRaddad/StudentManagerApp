

namespace GradeApp.Models
{
    internal class Student :Person
    {
        public int StudentId { get; set; }
        public void PrintStudentId()
        {
            Console.WriteLine($"Student Id: {StudentId}");
        }




    }
}
