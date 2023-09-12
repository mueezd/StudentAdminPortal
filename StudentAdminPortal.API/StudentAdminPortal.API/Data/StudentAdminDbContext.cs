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

        public DbSet<Student> Student { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Address> Address { get; set; }

    }
}
