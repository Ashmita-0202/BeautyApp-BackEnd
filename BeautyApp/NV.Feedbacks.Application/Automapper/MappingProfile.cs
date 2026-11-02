using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NV.Feedbacks.Application.Command.AddReview;
using NV.Feedbacks.Application.Feedbacks.Command.DeleteReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview;
using NV.Feedbacks.Application.Feedbacks.Queries.AllReview;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId;
using NV.Feedbacks.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Feedbacks.Application.Automapper
{
  public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Review
            CreateMap<AddReviewCommand, Review>().ReverseMap();
            CreateMap<AddReviewResponse, Review>().ReverseMap();
            CreateMap<GetAllReviewByQueryResponse, Review>().ReverseMap();
            CreateMap<PagedResult<Review>, PagedResult<GetAllReviewByQueryResponse>>().ReverseMap();
            CreateMap<DeleteReviewCommand, Review>().ReverseMap();
            CreateMap<GetReviewByIdQueryResponse, Review>().ReverseMap();
            CreateMap<GetReviewByBeauticianIdQueryResponse, Review>().ReverseMap();
            CreateMap<GetReviewByProductIdQueryResponse, Review>().ReverseMap();
            CreateMap<UpdateReviewCommand, Review>().ReverseMap();
            #endregion
        }
    }
}
