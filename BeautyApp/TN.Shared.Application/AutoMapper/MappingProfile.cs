using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Application.Shared.Command.UpdateExpiredDateItemStatusByCompany;
using TN.Shared.Application.Shared.Command.UpdateItemStatusByCompany;

namespace TN.Shared.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Inventory Settings
            CreateMap<UpdateItemStatusByCompanyResponse, UpdateItemStatusByCompanyCommand>().ReverseMap();

            #endregion

        }
    }
}
