namespace CustomerManagementERP.Models
{
    public class CustomerDeliveryAddress
    {
        public int CustomerDeliveryAddressId { get; set; }

        public int CustomerId { get; set; }
        public required string DeliveryAddress { get; set; }
        public required string ContactPerson { get; set; }
        public required string ContactPhone { get; set; }

        public Customer Customer { get; set; }
    }
}
