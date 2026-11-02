using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautician
{
    public record  UpdateBeauticianCommand
    (      
             string id,
            int experienceYear,
            string skills,
            string userId
    ) :IRequest<Result<UpdateBeauticianResponse>>;
}
