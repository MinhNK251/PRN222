namespace ServiceLayer.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public StudentDTO(int id, string name, string firstname, string lastname, string email, string phone) { 
            Id = id;
            Name = name;
        }
    }
}
