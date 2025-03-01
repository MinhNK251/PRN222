using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;
        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public StudentDTO Get(int id)
        {
            //Manual Mapping
            var studentEntity = _studentRepository.Get(id);
            return new StudentDTO(studentEntity.Id, studentEntity.Name, studentEntity.FirstName,
                studentEntity.LastName, studentEntity.Email, studentEntity.Phone);

            //Auto Mapping
        }
    }
}
