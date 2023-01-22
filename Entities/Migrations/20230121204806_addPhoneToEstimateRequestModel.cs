using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class addPhoneToEstimateRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13a61179-2b6c-420b-bf08-1b5de5acec3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7750b672-b3d6-4807-a9e5-d65211276d01");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "EstimateRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a21c161-a913-4b8c-a42b-b96923da30da", null, "Admin", "ADMIN" },
                    { "ec9af5d2-7d6c-46c8-8fe4-196612d2d6dd", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a21c161-a913-4b8c-a42b-b96923da30da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec9af5d2-7d6c-46c8-8fe4-196612d2d6dd");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "EstimateRequests");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13a61179-2b6c-420b-bf08-1b5de5acec3f", null, "Customer", "CUSTOMER" },
                    { "7750b672-b3d6-4807-a9e5-d65211276d01", null, "Admin", "ADMIN" }
                });
        }
    }
}
