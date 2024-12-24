namespace StudentApp.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public string Phone { get; set; }

        public bool Subscribe { get; set; }

        public Student()
        {
            
        }
    }
}
