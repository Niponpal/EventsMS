using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsMS.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEventFromFoodToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "FoodTokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodTokens_EventId",
                table: "FoodTokens",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTokens_Events_EventId",
                table: "FoodTokens",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodTokens_Events_EventId",
                table: "FoodTokens");

            migrationBuilder.DropIndex(
                name: "IX_FoodTokens_EventId",
                table: "FoodTokens");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "FoodTokens");
        }
    }
}
