using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NV.Payment.Application.Payment.Command.AddPayment;
using NV.Payment.Application.Payment.Command.DeletePayment;
using NV.Payment.Application.Payment.Command.UpdatePayment;
using NV.Payment.Application.Payment.Query.GetAllPayment;
using NV.Payment.Application.Payment.Query.GetPaymentById;
using NV.Payment.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Payment.Application.AutoMapper
{
    public class Automapper:Profile
    {
        public Automapper() 
        {
            #region Payment
            
            CreateMap<AddPaymentResponse, Payments>().ReverseMap();
            CreateMap<AddPaymentCommand, Payments>().ReverseMap();
            CreateMap<GetAllPaymentByQueryResponse, Payments>().ReverseMap();
            CreateMap<PagedResult<Payments>, PagedResult<GetAllPaymentByQueryResponse>>().ReverseMap();
            CreateMap<DeletePaymentCommand, Payments>().ReverseMap();
            CreateMap<GetPaymentByIdQueryResponse, Payments>().ReverseMap();
            CreateMap<UpdatePaymentByCommand, Payments>().ReverseMap();
            #endregion

        }
    }
}
