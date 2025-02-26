using RazorPagesSample.ServiceLayers.DTOs;

namespace RazorPagesSample.ServiceLayers
{
    public class StudentService
    {
        private readonly List<StudentDTO> _students = new List<StudentDTO> { new StudentDTO(1, "nguyen van a"), new StudentDTO(2, "tran thi b")};

        public StudentDTO Read(int id)
        {
            return _students.FirstOrDefault(m => m.StudentId == id);
        }
    }
}
