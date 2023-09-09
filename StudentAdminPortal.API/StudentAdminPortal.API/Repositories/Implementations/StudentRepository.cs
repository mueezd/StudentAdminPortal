using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace StudentAdminPortal.API.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminDbContext _dbContext;

        public StudentRepository(StudentAdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<StudentModel> GetStudents()
        {
             return _dbContext.Students.ToList();
        }


    }
}
