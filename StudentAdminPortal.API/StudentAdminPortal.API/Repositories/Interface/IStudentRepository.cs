using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAdminPortal.API.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
    }
}
