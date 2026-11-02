using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId
{
    public record  GetBookingByBeauticianIdQuery
  (string beauticianId):IRequest<Result<List<GetBookingByBeauticianIdQueryResponse>>>;
}
