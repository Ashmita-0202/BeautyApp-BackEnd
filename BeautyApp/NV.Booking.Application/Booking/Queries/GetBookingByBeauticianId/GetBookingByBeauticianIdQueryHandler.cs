using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Booking.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;


namespace NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId
{
    public class GetBookingByBeauticianIdQueryHandler:IRequestHandler<GetBookingByBeauticianIdQuery,Result<List<GetBookingByBeauticianIdQueryResponse>>>
    {
        private readonly IBookingsServices _bookingsServices;
        private readonly IMapper _mapper;

        public GetBookingByBeauticianIdQueryHandler(IBookingsServices bookingsServices,IMapper mapper)
        {
            _bookingsServices=bookingsServices;
            _mapper=mapper;
        }

        public async Task<Result<List<GetBookingByBeauticianIdQueryResponse>>> Handle(GetBookingByBeauticianIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingByBeauticianId = await _bookingsServices.GetBookingByBeauticianId(request.beauticianId, cancellationToken);
                return Result<List<GetBookingByBeauticianIdQueryResponse>>.Success(bookingByBeauticianId.Data);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching a Booking by Beautician id", ex);

            }
        }
    }
}
