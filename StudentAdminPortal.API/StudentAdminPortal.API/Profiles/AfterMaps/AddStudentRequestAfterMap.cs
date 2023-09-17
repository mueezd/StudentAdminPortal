using AutoMapper;
using StudentAdminPortal.API.Models.DomainModels;
using StudentAdminPortal.API.Models.DTO;
using System;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequestDto, Student>
    {
        public void Process(AddStudentRequestDto source, Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };

        }
    }
}
