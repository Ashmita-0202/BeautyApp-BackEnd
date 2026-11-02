using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.ServiceInterface
{
    public interface IServicesBeautician
    {
        Task<Result<AddServicesToBeauticianResponse>> Add(AddServicesToBeauticianCommand command);
    }
}
