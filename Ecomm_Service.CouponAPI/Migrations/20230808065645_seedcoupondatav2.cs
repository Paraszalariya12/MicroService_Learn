using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomm_Service.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedcoupondatav2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 2, "10OFF", 10.0, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupons",
                keyColumn: "CouponId",
                keyValue: 2);
        }
    }
}
