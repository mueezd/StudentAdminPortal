using System;

namespace StudentAdminPortal.API.Models.DomainModels
{
    public class AddressModel
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Navigation Propoerty
        public Guid StudentId { get; set; }
    }
}
