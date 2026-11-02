
using AutoMapper;
using NV.Booking.Application.Booking.Command.AddBooking;
using NV.Booking.Application.Booking.Command.DeleteBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking;
using NV.Booking.Application.Booking.Queries.GetAllBooking;
using NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId;
using NV.Booking.Application.Booking.Queries.GetBookingById;
using NV.Booking.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;


namespace NV.Booking.Application.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBookingCommand, AddBookingResponse>().ReverseMap();
            CreateMap<AddBookingResponse, Bookings>().ReverseMap();
            CreateMap<GetAllBookingByQueryResponse, Bookings>().ReverseMap();
            CreateMap<PagedResult<Bookings>, PagedResult<GetAllBookingByQueryResponse>>().ReverseMap();
            CreateMap<GetBookingByIdQueryResponse, Bookings>().ReverseMap();
            CreateMap<GetBookingByBeauticianIdQueryResponse, Bookings>().ReverseMap();
            CreateMap<UpdateBookingCommand, Bookings>().ReverseMap();
            CreateMap<DeleteBookingComand, Bookings>().ReverseMap();
        }
    }
}
