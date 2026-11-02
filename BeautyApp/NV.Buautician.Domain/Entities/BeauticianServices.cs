using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Domain.Primitive;

namespace NV.Buautician.Domain.Entities
{
    public class BeauticianServices : Entity
    {
        public BeauticianServices(
            string id,
            string beauticianId,
            string beautiServiceId
            ): base(id)
        {
            BeauticianId = beauticianId;
            BeautiServiceId = beautiServiceId;
        }

        public string BeauticianId { get; set; }    

        public virtual Beauticians Beauticians { get; set; }

        public string BeautiServiceId { get; set; }
        public virtual BeautyServices BeautyServices { get; set; }
    }
}
