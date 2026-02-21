using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsMS.Migrations
{
    /// <inheritdoc />
    public partial class @int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropIndex(
                name: "IX_FoodTokens_RegistrationId",
                table: "FoodTokens");

            migrationBuilder.AddColumn<long>(
                name: "FoodTokenId",
                table: "studentRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_studentRegistrations_FoodTokenId",
                table: "studentRegistrations",
                column: "FoodTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTokens_RegistrationId",
                table: "FoodTokens",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Slug",
                table: "Events",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_studentRegistrations_FoodTokens_FoodTokenId",
                table: "studentRegistrations",
                column: "FoodTokenId",
                principalTable: "FoodTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_studentRegistrations_FoodTokens_FoodTokenId",
                table: "studentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_studentRegistrations_FoodTokenId",
                table: "studentRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_FoodTokens_RegistrationId",
                table: "FoodTokens");

            migrationBuilder.DropIndex(
                name: "IX_Events_Slug",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FoodTokenId",
                table: "studentRegistrations");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    CanApprove = table.Column<bool>(type: "bit", nullable: false),
                    CanCreate = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_permissions_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodTokens_RegistrationId",
                table: "FoodTokens",
                column: "RegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissions_MenuId",
                table: "permissions",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id");
        }
    }
}
