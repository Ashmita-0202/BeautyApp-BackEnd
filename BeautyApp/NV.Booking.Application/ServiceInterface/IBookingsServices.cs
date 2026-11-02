using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Booking.Application.Booking.Command.AddBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking;
using NV.Booking.Application.Booking.Queries.GetAllBooking;
using NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId;
using NV.Booking.Application.Booking.Queries.GetBookingById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Booking.Application.ServiceInterface
{
    public interface IBookingsServices
    {
        Task<Result<AddBookingResponse>> Add(AddBookingCommand addBookingCommand);
        Task<Result<PagedResult<GetAllBookingByQueryResponse>>> GetAllBooking(PaginationRequest paginationRequest, CancellationToken cancellationToken = default);
        Task<Result<GetBookingByIdQueryResponse>> GetBookingById(string id, CancellationToken cancellationToken = default);
        Task<Result<List<GetBookingByBeauticianIdQueryResponse>>> GetBookingByBeauticianId(string beauticianId, CancellationToken cancellationToken = default);
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
        Task<Result<UpdateBookingResponse>> Update(string id, UpdateBookingCommand updateBookingCommand);
    }
}
