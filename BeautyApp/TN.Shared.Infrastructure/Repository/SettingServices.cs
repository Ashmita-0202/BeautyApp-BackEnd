using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Authentication.Application.Authentication.Queries.GetExpiredDateItemStatusByCompany;
using TN.Authentication.Domain.Entities;
using TN.Shared.Application.ServiceInterface;
using TN.Shared.Application.Shared.Command.UpdateExpiredDateItemStatusByCompany;
using TN.Shared.Application.Shared.Command.UpdateItemStatusByCompany;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace TN.Shared.Infrastructure.Repository
{
    public class SettingServices : ISettingServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public SettingServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<Result<GetItemStatusByCompanyResponse>> GetItemStatusByCompany(string companyId, CancellationToken cancellationToken)
        {
            try
            {
                var expiredDate = await _unitOfWork.BaseRepository<CompanySettings>().GetSingleAsync(x=>x.CompanyId == companyId);
                var expiredDateStatus = new GetItemStatusByCompanyResponse(expiredDate.ShowExpiredDateInItem, expiredDate.ShowSerialNumberInItem);
                return Result<GetItemStatusByCompanyResponse>.Success(expiredDateStatus);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch ExpiredDateItemStatus");
            }
        }

        public async Task<Result<UpdateItemStatusByCompanyResponse>> UpdateItemStatusByCompany(string companyId, bool isExpiredDate,bool isSerialNo, CancellationToken cancellationToken)
        {
            try
            {
                var itemStatus = await _unitOfWork.BaseRepository<CompanySettings>().GetSingleAsync(x => x.CompanyId == companyId);
                itemStatus.ShowExpiredDateInItem = isExpiredDate;
                itemStatus.ShowSerialNumberInItem = isSerialNo;

                _unitOfWork.BaseRepository<CompanySettings>().Update(itemStatus);
                await _unitOfWork.SaveChangesAsync();

                var expiredDateStatus = new UpdateItemStatusByCompanyResponse(itemStatus.CompanyId, itemStatus.ShowExpiredDateInItem, itemStatus.ShowSerialNumberInItem);
                return Result<UpdateItemStatusByCompanyResponse>.Success(expiredDateStatus);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch ExpiredDateItemStatus");
            }
        }
    }
}
