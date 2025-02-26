using Microsoft.AspNetCore.Mvc;

namespace RazorPagesSample.ServiceLayers.DTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public StudentDTO(int id, string studentName) { 
            StudentId = id;
            StudentName = studentName;
        }
    }
}
