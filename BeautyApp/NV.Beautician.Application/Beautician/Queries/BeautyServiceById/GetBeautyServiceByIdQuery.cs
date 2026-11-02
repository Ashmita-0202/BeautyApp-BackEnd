using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Queries.BeautyServiceById
{
    public record  GetBeautyServiceByIdQuery(string id) :IRequest<Result<GetBeautyServiceByIdQueryResponse>>;
    
}
