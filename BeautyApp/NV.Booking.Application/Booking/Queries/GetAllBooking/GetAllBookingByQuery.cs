using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Booking.Application.Booking.Queries.GetAllBooking
{
    public record GetAllBookingByQuery(PaginationRequest PaginationRequest) : IRequest<Result<PagedResult<GetAllBookingByQueryResponse>>>;

}
