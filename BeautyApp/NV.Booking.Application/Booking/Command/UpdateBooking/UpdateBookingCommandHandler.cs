using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Booking.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;


namespace NV.Booking.Application.Booking.Command.UpdateBooking
{
   public class UpdateBookingCommandHandler:IRequestHandler<UpdateBookingCommand, Result<UpdateBookingResponse>>
    {
        private readonly IBookingsServices _bookingServices;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateBookingCommand> _validator;

        public UpdateBookingCommandHandler(IBookingsServices bookingsServices,IMapper mapper,IValidator<UpdateBookingCommand> validator)
        {
            _bookingServices = bookingsServices;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<UpdateBookingResponse>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdateBookingResponse>.Failure(errors);

                }

                var updateBeautician = await _bookingServices.Update(request.id, request);

                if (updateBeautician.Errors.Any())
                {
                    var errors = string.Join(", ", updateBeautician.Errors);
                    return Result<UpdateBookingResponse>.Failure(errors);
                }

                if (updateBeautician is null || !updateBeautician.IsSuccess)
                {
                    return Result<UpdateBookingResponse>.Failure("Updates booking failed");
                }

                var beauticianDisplay = _mapper.Map<UpdateBookingResponse>(updateBeautician.Data);
                return Result<UpdateBookingResponse>.Success(beauticianDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating Booking by {request.id}", ex);
            }
        }
    }
}
