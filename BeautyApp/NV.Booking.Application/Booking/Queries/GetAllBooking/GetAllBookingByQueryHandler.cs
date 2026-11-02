using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Booking.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;


namespace NV.Booking.Application.Booking.Queries.GetAllBooking
{
    public class GetAllBookingByQueryHandler : IRequestHandler<GetAllBookingByQuery, Result<PagedResult<GetAllBookingByQueryResponse>>>
    {
        private readonly IBookingsServices _bookingsServices;
        private readonly IMapper _mapper;

        public GetAllBookingByQueryHandler(IBookingsServices bookingsServices, IMapper mapper)
        {
            _bookingsServices = bookingsServices;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllBookingByQueryResponse>>> Handle(GetAllBookingByQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allBooking = await _bookingsServices.GetAllBooking(request.PaginationRequest, cancellationToken);

                var allBookingDisplay = _mapper.Map<PagedResult<GetAllBookingByQueryResponse>>(allBooking.Data);

                return Result<PagedResult<GetAllBookingByQueryResponse>>.Success(allBookingDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while fetching Booking");
            }
        }
    }
}
