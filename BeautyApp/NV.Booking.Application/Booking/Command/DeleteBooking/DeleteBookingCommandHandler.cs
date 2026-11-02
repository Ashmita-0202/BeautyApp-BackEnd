using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Booking.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;


namespace NV.Booking.Application.Booking.Command.DeleteBooking
{
   public class DeleteBookingCommandHandler:IRequestHandler<DeleteBookingComand,Result<bool>>
    {
        private readonly IBookingsServices _bookingsServices;
        private readonly IMapper _mapper;

        public DeleteBookingCommandHandler(IBookingsServices bookingsServices,IMapper mapper) 
        { 
        
            _bookingsServices=bookingsServices;
            _mapper=mapper;
        
        }


        public async Task<Result<bool>> Handle(DeleteBookingComand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteBooking = await _bookingsServices.Delete(request.id, cancellationToken);
                if (deleteBooking is null)
                {
                    return Result<bool>.Failure("NotFound", "Booking not Found");
                }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting {request.id}", ex);
            }
        }
    }
}
