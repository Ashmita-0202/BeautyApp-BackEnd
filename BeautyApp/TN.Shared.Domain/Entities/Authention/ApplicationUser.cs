
using Microsoft.AspNetCore.Identity;
using NV.Buautician.Domain.Entities;

namespace TN.Authentication.Domain.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        //public string? DepartmentId { get; set; }
        //public string? ProfilePictureName { get; set; }
        //public string? ProfilePictureUrl { get; set; }
        //public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsBeautician { get; set; }
        //public ICollection<UserRole> UserRoles { get; set; }


        //Navigation Property
        public virtual ICollection<UserCompany> UserCompany { get; set; } = new List<UserCompany>();

        public virtual ICollection<Beauticians> Beauticians { get; set; } = new List<Beauticians>();
        
    }
}
