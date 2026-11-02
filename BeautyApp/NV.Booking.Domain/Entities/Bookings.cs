using NV.Buautician.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace NV.Booking.Domain.Entities
{
    public class Bookings : Entity
    {
        public Bookings(
            string id,
            string userId,
            string beauticianId,
            string beautyServiceId,
            string appointmentDate,
            ServiceStatus status,
            int totalClients,
            int durationInMinutes
            ) : base(id)
        {
            UserId = userId;
            BeauticianId = beauticianId;
            BeautyServiceId = beautyServiceId;
            AppointmentDate = appointmentDate;
            Status = status;
            TotalClients = totalClients;
            DurationInMinutes = durationInMinutes;
            
        }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string BeauticianId { get; set; }

        public virtual Beauticians Beautician { get; set; }

        public string BeautyServiceId { get; set; }
        public virtual BeautyServices BeautyServices { get; set; }

        public string AppointmentDate { get; set; }

        public ServiceStatus Status { get; set; }

        public enum ServiceStatus
        {
            Pending,
            Confirmed,
            Complete,
            Cancelled

        }
       
        [Range(1, 3, ErrorMessage = "You can only book for 1 to 3 clients.")]
        public int TotalClients { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
