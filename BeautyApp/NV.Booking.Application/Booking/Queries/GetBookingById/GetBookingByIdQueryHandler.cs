using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Booking.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;


namespace NV.Booking.Application.Booking.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Result<GetBookingByIdQueryResponse>>
    {
        private readonly IBookingsServices _bookingsServices;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingsServices bookingsServices, IMapper mapper)
        {
            _bookingsServices = bookingsServices;
            _mapper = mapper;

        }

        public async Task<Result<GetBookingByIdQueryResponse>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var bookingById = await _bookingsServices.GetBookingById(request.id);

                return Result<GetBookingByIdQueryResponse>.Success(bookingById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Booking by Id", ex);

            }
        }
    }
}
