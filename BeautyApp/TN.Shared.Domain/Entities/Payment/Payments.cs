
using NV.Booking.Domain.Entities;
using TN.Shared.Domain.Primitive;

namespace NV.Payment.Domain.Entities
{
    public class Payments : Entity
    {
        public Payments(
            string id,
            string bookingId,
            string amount,
            string paymentMethod,
            PaymentStatus status 
            ): base(id)
        {
            BookingId = bookingId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            Status = status;

        }

        public string BookingId { get; set; }
        public virtual Bookings Booking { get; set; }

        public string Amount { get; set; }
        public string PaymentMethod { get; set; }

        public PaymentStatus Status {  get; set; }

        public enum PaymentStatus
        {
            Pending,
            Paid,
            Failed
        }
    }
}
