using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsMS.Migrations
{
    /// <inheritdoc />
    public partial class FixRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentHistories_payments_PaymentId",
                table: "paymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_paymentHistories_PaymentId",
                table: "paymentHistories");

            migrationBuilder.CreateIndex(
                name: "IX_paymentHistories_PaymentId",
                table: "paymentHistories",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentHistories_payments_PaymentId",
                table: "paymentHistories",
                column: "PaymentId",
                principalTable: "payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentHistories_payments_PaymentId",
                table: "paymentHistories");

            migrationBuilder.DropIndex(
                name: "IX_paymentHistories_PaymentId",
                table: "paymentHistories");

            migrationBuilder.CreateIndex(
                name: "IX_paymentHistories_PaymentId",
                table: "paymentHistories",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_paymentHistories_payments_PaymentId",
                table: "paymentHistories",
                column: "PaymentId",
                principalTable: "payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
