using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Data;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Models.DTO;
using StudentAdminPortal.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminDbContext _dbContext;

        public StudentRepository(StudentAdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task<Student> GetStudentByIdAsync(Guid studentId)
        {
            return await _dbContext.Student
                .Include(nameof(Gender)).Include(nameof(Address))
                .FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
             return await _dbContext.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }
    }
}
