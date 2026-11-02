using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Payment.Application.Payment.Command.AddPayment;
using NV.Payment.Application.Payment.Command.UpdatePayment;
using NV.Payment.Application.Payment.Query.GetAllPayment;
using NV.Payment.Application.Payment.Query.GetPaymentById;
using NV.Payment.Application.ServiceInterface;
using NV.Payment.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Payment.Infrastructure.ServiceImpl
{
    public class PaymentServices:IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentServices(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        
        }

        public async Task<Result<AddPaymentResponse>> AddPayment(AddPaymentCommand command)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    string newId = Guid.NewGuid().ToString();
                    var paymentData = new Payments
                    (
                            newId,
                            command.bookingId,
                            command.amount,
                            command.paymentMethod,
                            command.status
                            
                    );

                    await _unitOfWork.BaseRepository<Payments>().AddAsync(paymentData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddPaymentResponse>(paymentData);
                    return Result<AddPaymentResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding paymenet ", ex);

                }
            }
        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await _unitOfWork.BaseRepository<Payments>().GetByGuIdAsync(id);
                if (payment is null)
                {
                    return Result<bool>.Failure("NotFound", "payment Cannot be Found");
                }

                _unitOfWork.BaseRepository<Payments>().Delete(payment);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting payment having {id}", ex);
            }

        }

        public async Task<Result<PagedResult<GetAllPaymentByQueryResponse>>> GetAllPayment(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _unitOfWork.BaseRepository<Payments>().GetAllAsyncWithPagination();

                var paymentPagedResult = await payment.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);


                var allpaymentResponse = _mapper.Map<PagedResult<GetAllPaymentByQueryResponse>>(paymentPagedResult.Data);
                return Result<PagedResult<GetAllPaymentByQueryResponse>>.Success(allpaymentResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all payment ", ex);
            }
        }

        public async Task<Result<GetPaymentByIdQueryResponse>> GetPaymentById(string id, CancellationToken cancellationToken = default)
        {
            try
            {

                var payments = await _unitOfWork.BaseRepository<Payments>().GetByGuIdAsync(id);
                var paymentsResponse = _mapper.Map<GetPaymentByIdQueryResponse>(payments);
                return Result<GetPaymentByIdQueryResponse>.Success(paymentsResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching payment by using Id", ex);
            }
        }

        public async Task<Result<UpdatePaymentResponse>> Update(string id, UpdatePaymentByCommand updatePaymentByCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdatePaymentResponse>.Failure("NotFound", "Please provide valid Payment Id");
                    }

                    var paymentToBeUpdated = await _unitOfWork.BaseRepository<Payments>().GetByGuIdAsync(id);
                    if (paymentToBeUpdated is null)
                    {
                        return Result<UpdatePaymentResponse>.Failure("NotFound", "Payments are not Found");
                    }

                    _mapper.Map(updatePaymentByCommand, paymentToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdatePaymentResponse
                        (

                              id,
                              paymentToBeUpdated.BookingId,
                              paymentToBeUpdated.Amount,
                              paymentToBeUpdated.PaymentMethod,
                              paymentToBeUpdated.Status
                            
                        );

                    return Result<UpdatePaymentResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Payment", ex);
                }

            }
        }
    }
}
