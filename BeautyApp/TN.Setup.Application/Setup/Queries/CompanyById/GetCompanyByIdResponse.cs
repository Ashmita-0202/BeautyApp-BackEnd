using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TN.Setup.Application.Setup.Queries.CompanyById
{
     public record GetCompanyByIdResponse
     (
         
            string id="",
            string name="",
            string address = "",
            string email="",
            string contactNumber = "",
            string pan = "",
            string imageUrl="",
            bool isEnabled=true,
            DateTime createdDate=default,
            string createdBy = "",
            DateTime modifiedDate = default,
            string modifiedBy = "",
            bool isDeleted = true

     );
    
   
}
