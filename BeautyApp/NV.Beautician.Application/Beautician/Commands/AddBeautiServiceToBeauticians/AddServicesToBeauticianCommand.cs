using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians
{
    public record  AddServicesToBeauticianCommand
    (
          string beauticianId,
           List<string> beautiServiceId

    ) :IRequest<Result<AddServicesToBeauticianResponse>>;
}
