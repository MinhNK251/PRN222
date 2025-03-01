using RazorPagesSample.ServiceLayers.DTOs;

namespace RazorPagesSample.ServiceLayers
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public StudentDTO Read(int id)
        {
            var studentEntity = _studentRepository.Read(id);
            return new StudentDTO(studentEntity.StudentId, studentEntity.StudentName);
        }
    }
}
