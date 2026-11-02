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


namespace NV.Booking.Application.Booking.Command.AddBooking
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Result<AddBookingResponse>>
    {
        private readonly IBookingsServices _bookingsServices;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBookingCommand> _validator;

        public AddBookingCommandHandler(IBookingsServices bookingsServices, IMapper mapper, IValidator<AddBookingCommand> validator)
        {
            _bookingsServices = bookingsServices;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<AddBookingResponse>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddBookingResponse>.Failure(errors);
                }

                var addBooking = await _bookingsServices.Add(request);

                if (addBooking.Errors.Any())
                {
                    var errors = string.Join(", ", addBooking.Errors);
                    return Result<AddBookingResponse>.Failure(errors);
                }

                if (addBooking is null || !addBooking.IsSuccess)
                {
                    return Result<AddBookingResponse>.Failure(" ");
                }

                var bookingDisplay = _mapper.Map<AddBookingResponse>(addBooking.Data);
                return Result<AddBookingResponse>.Success(bookingDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Bookings", ex);

            }
        }
    }
}
