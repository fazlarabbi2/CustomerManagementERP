using System.ComponentModel.DataAnnotations;

namespace CustomerManagementERP.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Customer Name is Required"), StringLength(30, ErrorMessage = "Customer Name cannot exceed 30 characters")]
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime BuisenesStartDate { get; set; }
        public string Phone { get; set; }
        public decimal CreditLimit { get; set; }

        public ICollection<CustomerDeliveryAddress> DeliveryAddresses { get; set; }
    }
}
