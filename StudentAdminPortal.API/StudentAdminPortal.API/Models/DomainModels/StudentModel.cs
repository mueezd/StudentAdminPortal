using StudentAdminPortal.API.Models.DomainModels;
using System;

namespace StudentAdminPortal.API.Models
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string ProfileImageUrl { get; set; }
        public Guid GenderId { get; set; }

        //Navigation Properties

        public GenderModel Gender { get; set; }
        public AddressModel Address { get; set; }
    }
}
