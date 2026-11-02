using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.DeleteBeautician;
using NV.Beautician.Application.Beautician.Commands.DeleteBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService;
using NV.Beautician.Application.Beautician.Queries.AllBeautyServices;
using NV.Beautician.Application.Beautician.Queries.BeautyServiceById;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using NV.Buautician.Domain.Entities;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Beautician
            CreateMap<GetAllBeauticianByQueryResponse, Beauticians>().ReverseMap();
            CreateMap<PagedResult<Beauticians>, PagedResult<GetAllBeauticianByQueryResponse>>().ReverseMap();
            CreateMap<AddBeauticianResponse, Beauticians>().ReverseMap();
            CreateMap<AddBeauticianCommand, Beauticians>().ReverseMap();
            CreateMap<GetBeauticianByIdQueryResponse, Beauticians>().ReverseMap();
            CreateMap<UpdateBeauticianCommand, Beauticians>().ReverseMap();
            CreateMap<DeleteBeauticianCommand, Beauticians>().ReverseMap();
            #endregion

            #region BeautyServices
            CreateMap<AddBeautyServicesResponse, BeautyServices>().ReverseMap();
            CreateMap<AddBeautyServicesCommand, BeautyServices>().ReverseMap();
            CreateMap<GetAllBeautyServiceByQueryResponse, BeautyServices>().ReverseMap();
            CreateMap<PagedResult<BeautyServices>, PagedResult<GetAllBeautyServiceByQueryResponse>>().ReverseMap();
            CreateMap<GetBeautyServiceByIdQueryResponse, BeautyServices>().ReverseMap();
            CreateMap<UpdateBeautyServiceCommand, BeautyServices>().ReverseMap();
            CreateMap<DeleteBeautyServiceCommand, BeautyServices>().ReverseMap();
            #endregion


            #region BeautiServiceToBeautician
            CreateMap<AddServicesToBeauticianResponse, AddServicesToBeauticianCommand>().ReverseMap();
            CreateMap<AddServicesToBeauticianResponse, BeauticianServices>().ReverseMap();
            #endregion

        }

    }
}

//hello