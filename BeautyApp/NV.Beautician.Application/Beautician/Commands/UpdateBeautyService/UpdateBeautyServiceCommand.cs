using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautyService
{
    public record  UpdateBeautyServiceCommand
  (
            string id,
            string name,
            string description,
            decimal price,
            int durationInMinutes
    ) :IRequest<Result<UpdateBeautyServiceResponse>>;
}
