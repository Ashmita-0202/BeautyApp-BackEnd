
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.IRepository;
using static TN.Authentication.Domain.Entities.Company;

namespace TN.Shared.Infrastructure.Repository
{
    public class BillNumberGenerator : IBillNumberGenerator
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillNumberGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public async Task<string> GenerateBillNumberAsync( string companyId, string billType)
        {
            try
            {
                var company = await _unitOfWork.BaseRepository<Company>().GetByGuIdAsync(companyId);
                string companyShortName = company.Name.Substring(0, 3).ToUpper();
                string yearMonth = DateTime.UtcNow.ToString("yyyyMM");
                string prefix = $"{companyShortName}-{(billType).Substring(0,3).ToUpper()}-{yearMonth}";
                //if (billType == "purchase")
                //{
                  
                //    if (company.BillNumberGenerationForService == BillNumberGenerationType.Manual)
                //    {
                //        throw new Exception("Bill number should be entered manually.");
                //    }


                //}
                //else
                //{

                //    if (company.BillNumberGenerationForService == BillNumberGenerationType.Manual)
                //    {
                //        throw new Exception("Bill number should be entered manually.");
                //    }

       
                //}

              
                return string.Empty;

            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while creating Bill Number by companyId {companyId}");
            }
        }

 

    }
}
