using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.DeleteBeautician
{
   public record class DeleteBeauticianCommand
   (string id):IRequest<Result<bool>>;

}
