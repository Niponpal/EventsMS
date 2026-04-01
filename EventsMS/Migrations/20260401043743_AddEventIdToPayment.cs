using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsMS.Migrations
{
    /// <inheritdoc />
    public partial class AddEventIdToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e77eadcd-445f-4248-a5dd-6060c7a8ec7b", "AQAAAAIAAYagAAAAENgF0aWinKSPU22bHKqeLU4YEjk23gsv1jPAltNf8uS7gv+PKCI2sCueiLf4onvwtA==", "96e6ded9-f2e3-4755-bc94-578681e18392" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a28416fc-5315-40e0-98f2-cfe1d374b940", "AQAAAAIAAYagAAAAELSlaw101fhMvWo1dtQ1VgdbszsiGah4DvXFIc8N3K5Uig6CJEZAyW11GQ6rZjHu2Q==", "7e75c58c-1ecb-4406-af38-d53c2b010db8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f554e5a2-f4c0-4da3-b852-a924c400b3c5", "AQAAAAIAAYagAAAAEJsathZjE3UX45tAWrlv7asV8dHgqiwLEVv1Aca3xlFRkyy4+L5Ap72luT67MZfH4g==", "147b406c-10ac-4682-b5a7-0feb3f4a386d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Payments");

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
    }
}
