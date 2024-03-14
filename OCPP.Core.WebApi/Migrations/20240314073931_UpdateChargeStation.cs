using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCPP.Core.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChargeStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ConnectorStatuses_ConnectorId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ConnectorId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ConnectorId1",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "ConnectorStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ConnectorId",
                table: "Transactions",
                column: "ConnectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ConnectorStatuses_ConnectorId",
                table: "Transactions",
                column: "ConnectorId",
                principalTable: "ConnectorStatuses",
                principalColumn: "ConnectorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ConnectorStatuses_ConnectorId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ConnectorId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "ConnectorId1",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "ConnectorStatuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ConnectorId1",
                table: "Transactions",
                column: "ConnectorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ConnectorStatuses_ConnectorId1",
                table: "Transactions",
                column: "ConnectorId1",
                principalTable: "ConnectorStatuses",
                principalColumn: "ConnectorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
