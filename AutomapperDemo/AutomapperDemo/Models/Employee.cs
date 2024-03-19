namespace AutomapperDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        // Sensitive Information
        public string SocialSecurityNumber { get; set; }
        public decimal Salary { get; set; }
    }
}