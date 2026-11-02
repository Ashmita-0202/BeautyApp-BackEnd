using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Booking.Application.Booking.Command.AddBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking;
using NV.Booking.Application.Booking.Queries.GetAllBooking;
using NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId;
using NV.Booking.Application.Booking.Queries.GetBookingById;
using NV.Booking.Application.ServiceInterface;
using NV.Booking.Domain.Entities;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Booking.Infrastructure.ServiceImpl
{
    public class BookingsServices : IBookingsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public BookingsServices(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;

        }

        public async Task<Result<AddBookingResponse>> Add(AddBookingCommand addBookingCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string userId = _tokenService.GetUserId();
                    string newId = Guid.NewGuid().ToString();
                    if (addBookingCommand.totalClients < 1 || addBookingCommand.totalClients > 3)
                    {
                        return Result<AddBookingResponse>.Failure("You can only book for 1 to 3 clients.");
                    }

                    var beautyService = await _unitOfWork.BaseRepository<BeautyServices>()
                        .GetByGuIdAsync(addBookingCommand.beautyServiceId);

                    if (beautyService == null)
                    {
                        return Result<AddBookingResponse>.Failure("Beauty service not found.");
                    }

                    int totalDuration = beautyService.DurationInMinutes * addBookingCommand.totalClients;
                    var bookingData = new Bookings
                    (
                            newId,
                            userId,
                            addBookingCommand.beauticianId,
                            addBookingCommand.beautyServiceId,
                            addBookingCommand.appointmentDate,
                            addBookingCommand.status,
                           addBookingCommand.totalClients,
                            totalDuration
                    );

                    await _unitOfWork.BaseRepository<Bookings>().AddAsync(bookingData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddBookingResponse>(bookingData);
                    return Result<AddBookingResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Bookings ", ex);

                }
            }
        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _unitOfWork.BaseRepository<Bookings>().GetByGuIdAsync(id);
                if (booking is null)
                {
                    return Result<bool>.Failure("NotFound", "booking Cannot be Found");
                }

                _unitOfWork.BaseRepository<Bookings>().Delete(booking);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting booking having {id}", ex);
            }

        }

        public async Task<Result<PagedResult<GetAllBookingByQueryResponse>>> GetAllBooking(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var booking = await _unitOfWork.BaseRepository<Bookings>().GetAllAsyncWithPagination();

                var bookingPagedResult = await booking.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);

                var allBookingResult = _mapper.Map<PagedResult<GetAllBookingByQueryResponse>>(bookingPagedResult.Data);
                return Result<PagedResult<GetAllBookingByQueryResponse>>.Success(allBookingResult);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all Booking ", ex);
            }


        }

        public async Task<Result<List<GetBookingByBeauticianIdQueryResponse>>> GetBookingByBeauticianId(string beauticianId, CancellationToken cancellationToken = default)
        {
            try
            {
                var booking = await _unitOfWork.BaseRepository<Bookings>().GetConditionalAsync(x => x.BeauticianId == beauticianId);
                if (booking is null)
                {
                    return Result<List<GetBookingByBeauticianIdQueryResponse>>.Failure("NotFound", "Review Data are not found");
                }

                var bookingResponse = _mapper.Map<List<GetBookingByBeauticianIdQueryResponse>>(booking);

                return Result<List<GetBookingByBeauticianIdQueryResponse>>.Success(bookingResponse);


            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching Review By Beautician Id", ex);

            }
        }

        public async Task<Result<GetBookingByIdQueryResponse>> GetBookingById(string id, CancellationToken cancellationToken = default)
        {
            try
            {

                var booking = await _unitOfWork.BaseRepository<Bookings>().GetByGuIdAsync(id);
                var bookingResponse = _mapper.Map<GetBookingByIdQueryResponse>(booking);
                return Result<GetBookingByIdQueryResponse>.Success(bookingResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching booking by using Id", ex);
            }
        }

        public async Task<Result<UpdateBookingResponse>> Update(string id, UpdateBookingCommand updateBookingCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateBookingResponse>.Failure("NotFound", "Please provide valid booking Id");
                    }

                    var bookingToBeUpdated = await _unitOfWork.BaseRepository<Bookings>().GetByGuIdAsync(id);
                    if (bookingToBeUpdated is null)
                    {
                        return Result<UpdateBookingResponse>.Failure("NotFound", "bookings are not Found");
                    }

                    _mapper.Map(updateBookingCommand, bookingToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateBookingResponse
                        (

                          id,
                          bookingToBeUpdated.UserId,
                          bookingToBeUpdated.BeauticianId,
                          bookingToBeUpdated.BeautyServiceId,
                          bookingToBeUpdated.AppointmentDate
                          //bookingToBeUpdated.Status
                         
                        );

                    return Result<UpdateBookingResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Bookings", ex);
                }

            }
        }
    }
}