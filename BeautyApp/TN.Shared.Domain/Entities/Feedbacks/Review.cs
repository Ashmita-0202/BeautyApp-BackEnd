using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NV.Buautician.Domain.Entities;
using NV.Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace NV.Feedbacks.Domain.Entities
{
    public class Review : Entity
    {
        
        public Review(
            string id,
            string userId,
            string beauticianId,
            double rating,
            string comments,
            string? productId
            ) : base(id)
        {
            UserId = userId;
            BeauticianId = beauticianId;
            Rating = rating;
            Comments = comments;
            ProductsId = productId;

        }

        public string? UserId { get; set; }  
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Beautician")]
        public string? BeauticianId { get; set; }

        public virtual Beauticians Beautician { get; set; }

        public double Rating {get; set; }
        public string Comments { get; set; }

        public string? ProductsId { get; set; } 
        public virtual Product Products { get; set; } 
    }
}
