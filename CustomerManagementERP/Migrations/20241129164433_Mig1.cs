using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagementERP.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "deliveryAddresses",
                columns: table => new
                {
                    CustomerDeliveryAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryAddresses", x => x.CustomerDeliveryAddressId);
                    table.ForeignKey(
                        name: "FK_deliveryAddresses_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "CustomerId", "Address", "CreditLimit", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", 1000m, "John Doe", "123-456-7890" },
                    { 2, "456 Elm St", 2000m, "Jane Smith", "987-654-3210" },
                    { 3, "789 Oak St", 1500m, "Mike Johnson", "456-123-7890" }
                });

            migrationBuilder.InsertData(
                table: "deliveryAddresses",
                columns: new[] { "CustomerDeliveryAddressId", "ContactPerson", "ContactPhone", "CustomerId", "DeliveryAddress" },
                values: new object[,]
                {
                    { 1, "John Doe", "123-456-7890", 1, "123 Main St - Apt 1" },
                    { 2, "Jane Smith", "987-654-3210", 2, "456 Elm St - Office" },
                    { 3, "Mike Johnson", "456-123-7890", 3, "789 Oak St - Suite 300" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_deliveryAddresses_CustomerId",
                table: "deliveryAddresses",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliveryAddresses");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
