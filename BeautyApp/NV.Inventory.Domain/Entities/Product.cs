
using TN.Shared.Domain.Primitive;

namespace NV.Inventory.Domain.Entities
{
    public class Product : Entity    {
        public Product(
            string id,
            string name,
            int quantity,
            decimal price,
            DateTime latestUpdated
            ) : base(id)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            LatestUpdated = latestUpdated;
            
        }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime LatestUpdated { get; set; }
        //public ICollection<Review> Reviews { get; set; } // Navigation property
    }
}
