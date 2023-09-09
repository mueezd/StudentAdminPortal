using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Models.DomainModels;

namespace StudentAdminPortal.API.Data
{
    public class StudentAdminDbContext : DbContext
    {
        public StudentAdminDbContext(DbContextOptions<StudentAdminDbContext> options) : base(options)
        {

        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<GenderModel> Genders { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }

    }
}
