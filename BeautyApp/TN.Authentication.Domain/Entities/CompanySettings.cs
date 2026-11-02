using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Domain.Primitive;

namespace TN.Authentication.Domain.Entities
{
    public class CompanySettings: Entity
    {
        public CompanySettings(): base(null)
        {
            
        }

        public CompanySettings(
            string id,
            bool showExpiredDateInItem,
            bool showSerialNumberInItem,
            string companyId
            ): base(id)
        {
            ShowExpiredDateInItem = showExpiredDateInItem;
            ShowSerialNumberInItem = showSerialNumberInItem;
            CompanyId = companyId;
            
            

            
        }

        public bool ShowExpiredDateInItem { get; set; }
        public bool ShowSerialNumberInItem { get;set; }
        public string CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
