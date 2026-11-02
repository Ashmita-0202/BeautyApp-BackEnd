
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace NV.Buautician.Domain.Entities
{
    public class Beauticians : Entity
    {

        public Beauticians(
            string id,
            int experienceYear,
            string skills,
            string userId
            ): 
            base(id)
        {
            ExperienceYear = experienceYear;
            Skills = skills;
            UserId = userId;
            BeauticianServices = new List<BeauticianServices>();   
        }

        public int ExperienceYear {  get; set; }
      
        public string Skills { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<BeauticianServices> BeauticianServices { get; set; }
    }
}
