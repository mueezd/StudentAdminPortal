using System;

namespace StudentAdminPortal.API.Models.DomainModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Navigation Propoerty
        public Guid StudentId { get; set; }
    }
}
