using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using FluentValidation;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.IRepository;

namespace NV.Beautician.Infrastructure.ServiceImpl
{
    public class ServicesBeauticians : IServicesBeautician
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ServicesBeauticians(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService=tokenService;
        }

        public async Task<Result<AddServicesToBeauticianResponse>> Add(AddServicesToBeauticianCommand command)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var existingServicesBeautician = await _unitOfWork.BaseRepository<BeauticianServices>()
                        .GetConditionalAsync(x => x.BeauticianId == command.beauticianId);

                    if (existingServicesBeautician.Any())
                    {
                        _unitOfWork.BaseRepository<BeauticianServices>().DeleteRange(existingServicesBeautician.ToList());
                        await _unitOfWork.SaveChangesAsync();
                    }

                    var newServicesBeautician = command.beautiServiceId.Select(beautiServiceId =>
                        new BeauticianServices(
                            Guid.NewGuid().ToString(), 
                            command.beauticianId,
                            beautiServiceId
                        )
                    ).ToList();

                    if (newServicesBeautician.Any())
                    {
                        await _unitOfWork.BaseRepository<BeauticianServices>().AddRange(newServicesBeautician);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    scope.Complete();
                    var userDisplay = _mapper.Map<AddServicesToBeauticianResponse>(command);
                    return Result<AddServicesToBeauticianResponse>.Success(userDisplay);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("Something went wrong while adding beautiservice to beautician", ex);
                }
            }
        }
    }
}
