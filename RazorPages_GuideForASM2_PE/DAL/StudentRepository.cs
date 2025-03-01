using DAL;
using RazorPagesSample.ServiceLayers.DTOs;

namespace RazorPagesSample.ServiceLayers
{
    public class StudentRepository
    {
        private readonly StudentDBContext _dbContext;

        public StudentRepository(StudentDBContext dbContext) {
            _dbContext = dbContext;
        }

        public Student Read(int id)
        {
            return _dbContext.Students.FirstOrDefault(m => m.StudentId == id);
        }
    }
}
