namespace MyFirstWebAPIProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }

    public enum Department
    {
        HR, IT, Finance, Marketing
    }
}