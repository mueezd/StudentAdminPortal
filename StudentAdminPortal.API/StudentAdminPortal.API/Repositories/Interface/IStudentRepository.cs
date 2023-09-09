using StudentAdminPortal.API.Models;
using System.Collections.Generic;

namespace StudentAdminPortal.API.Repositories.Interface
{
    public interface IStudentRepository
    {
        List<StudentModel> GetStudents();
    }
}
