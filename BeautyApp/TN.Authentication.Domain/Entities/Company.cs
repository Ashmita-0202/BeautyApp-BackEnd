using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Setup.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace TN.Authentication.Domain.Entities
{
    public class Company: Entity
    {
        public Company() : base(null)
        {


        }

        public Company(
            string id,
            string name,
            string address,
            string email,
            string contactNumber,
            string pan,
            string imageUrl,
            bool isEnabled,
            DateTime createdDate,
            string createdBy,
            DateTime modifiedDate,
            string modifiedBy,
            bool isDeleted,
            string organization

            ) : base(id)
        {
            Name = name;
            Address = address;
            Email = email;
            ContactNumber = contactNumber;
            PAN = pan;
            ImageUrl = imageUrl;
            IsEnabled = isEnabled;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            ModifiedDate = modifiedDate;
            ModifiedBy = modifiedBy;
            IsDeleted = isDeleted;
            OrganizationId = organization;
            UserCompany = new List<UserCompany>();
            CompanySettings = new List<CompanySettings>();

        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string PAN { get; set; }
        public string ImageUrl { get; set; }
        public bool IsEnabled { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

        public string OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public virtual ICollection<UserCompany> UserCompany { get; set; }

        public virtual ICollection<CompanySettings> CompanySettings { get; set; }

    }
}
