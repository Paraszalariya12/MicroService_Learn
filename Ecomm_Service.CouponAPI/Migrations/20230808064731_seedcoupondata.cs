using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecomm_Service.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedcoupondata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 1, "50OFF", 50.0, 20 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupons",
                keyColumn: "CouponId",
                keyValue: 1);
        }
    }
}
