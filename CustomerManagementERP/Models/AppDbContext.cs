using Microsoft.EntityFrameworkCore;

namespace CustomerManagementERP.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "John Doe",
                    Address = "123 Main St",
                    Phone = "123-456-7890",
                    CreditLimit = 1000,
                    BuisenesStartDate = new DateTime(2015, 1, 15)
                },
                new Customer { CustomerId = 2, Name = "Jane Smith", Address = "456 Elm St", Phone = "987-654-3210", CreditLimit = 2000, BuisenesStartDate = new DateTime(2018, 1, 15) },
                new Customer { CustomerId = 3, Name = "Mike Johnson", Address = "789 Oak St", Phone = "456-123-7890", CreditLimit = 1500, BuisenesStartDate = new DateTime(2014, 12, 14) }
            );

            // Seed Customer Delivery Addresses
            modelBuilder.Entity<CustomerDeliveryAddress>().HasData(
                new CustomerDeliveryAddress { CustomerDeliveryAddressId = 1, CustomerId = 1, DeliveryAddress = "123 Main St - Apt 1", ContactPerson = "John Doe", ContactPhone = "123-456-7890" },
                new CustomerDeliveryAddress { CustomerDeliveryAddressId = 2, CustomerId = 2, DeliveryAddress = "456 Elm St - Office", ContactPerson = "Jane Smith", ContactPhone = "987-654-3210" },
                new CustomerDeliveryAddress { CustomerDeliveryAddressId = 3, CustomerId = 3, DeliveryAddress = "789 Oak St - Suite 300", ContactPerson = "Mike Johnson", ContactPhone = "456-123-7890" }
            );
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerDeliveryAddress> deliveryAddresses { get; set; }
    }
}
