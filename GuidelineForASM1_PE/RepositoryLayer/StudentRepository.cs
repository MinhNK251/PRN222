using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Models;

namespace RepositoryLayer
{
    public class StudentRepository
    {
        private readonly StudentDbContext _dbContext;
        public StudentRepository(StudentDbContext dbContext) {
            _dbContext = dbContext;
        }
        public Student Get(int id)
        {
            return _dbContext.Students.FirstOrDefault(m => m.Id == id);
        }
    }
}
