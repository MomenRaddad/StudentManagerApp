
namespace GradeApp.Models
{
    internal class Person
    {
        internal string Name { get; set; }
        internal int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public void Introduce()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Date of Birth: {DateOfBirth}");
        }

    }
}