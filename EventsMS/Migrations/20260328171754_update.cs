using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsMS.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f73d10f-0e10-4372-9ab6-67f60b17bd65", "AQAAAAIAAYagAAAAEEO+iuwRGFc4q2+wBW9F4/bJ2R0LWBWGP2GZlAF1un6xkBEv9fE9M5flb+WbQPX61w==", "3cef7363-354c-4023-af3b-c3196b48d265" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa5f52b9-3bbb-4492-999b-6a0d79efb616", "AQAAAAIAAYagAAAAEKUNJZfkmxDYLc5ewCDfKluUfMqAWr1o7fbq0xDO9fWab2GOOX4pHeKlDby6BDqdIw==", "338bca42-5b8f-4b65-b5de-8a828d0f3f7a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7d79fcb-49b5-4f26-a7de-5e834e3ff54c", "AQAAAAIAAYagAAAAEM7QZXv6C1fRW0jYZp0A36o4qklW3TOkCox29UGSZsTUr5y8BoPl0KqEFgMHtOBlyg==", "1976e371-f07a-4272-bf6b-bfedbb7aeb86" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e81b46c-3bf8-428c-8af9-41e80bed5e92", "AQAAAAIAAYagAAAAEMatL/qykECvk9eSUVq5Ol9cAdcqFPzmHOAtMPqfVyknSA18YlRXIfxQbGbKYItQSg==", "0a1b487c-b2b8-4ead-99b2-a69a4d10c254" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f86b15ed-1996-4ea3-be49-d4fcde1199be", "AQAAAAIAAYagAAAAEBAaZL9grCEH36LUh3a2tuBAlQ7HCd3zUf8zGed+b4qcuZuho30V+5nvqh9DoE/OPQ==", "f81f2561-2e69-4feb-b321-52fce6a38435" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72a0e717-da70-4624-a451-ac64ac26762a", "AQAAAAIAAYagAAAAEEuIFrucY6dDi9qV3a3D7MtRjkIP2fwRbkqbfBLhyvrn7coUeHtqSm/hBnbNrdM7+A==", "75656885-31cd-4100-b2b4-42a86a5b2de1" });
        }
    }
}
