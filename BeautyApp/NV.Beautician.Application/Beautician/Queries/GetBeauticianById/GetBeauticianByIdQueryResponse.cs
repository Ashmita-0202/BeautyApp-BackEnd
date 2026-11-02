using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Queries.GetBeauticianById
{
    public record  GetBeauticianByIdQueryResponse
    (
            string id,
            string name,
            int experienceYear,
            string skills,
            string userId
    );
}
