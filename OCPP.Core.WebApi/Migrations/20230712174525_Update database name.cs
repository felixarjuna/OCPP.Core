using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCPP.Core.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabasename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChargePoint",
                table: "ChargePoint");

            migrationBuilder.RenameTable(
                name: "ChargePoint",
                newName: "ChargeStations");

            migrationBuilder.RenameIndex(
                name: "ChargePoint_Identifier",
                table: "ChargeStations",
                newName: "ChargeStation_Identifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChargeStations",
                table: "ChargeStations",
                column: "ChargeStationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChargeStations",
                table: "ChargeStations");

            migrationBuilder.RenameTable(
                name: "ChargeStations",
                newName: "ChargePoint");

            migrationBuilder.RenameIndex(
                name: "ChargeStation_Identifier",
                table: "ChargePoint",
                newName: "ChargePoint_Identifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChargePoint",
                table: "ChargePoint",
                column: "ChargeStationId");
        }
    }
}
