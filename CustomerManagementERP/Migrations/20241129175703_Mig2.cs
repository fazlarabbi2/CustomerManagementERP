using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerManagementERP.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BuisenesStartDate",
                table: "customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "BuisenesStartDate",
                value: new DateTime(2015, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "BuisenesStartDate",
                value: new DateTime(2018, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "BuisenesStartDate",
                value: new DateTime(2014, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuisenesStartDate",
                table: "customers");
        }
    }
}
