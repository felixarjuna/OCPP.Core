using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCPP.Core.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChargeStationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "ChargeStations");

            migrationBuilder.AlterColumn<string>(
                name: "VendorName",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirmwareVersion",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChargeBoxSerialNumber",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ChargePointSerialNumber",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeterSerialNumber",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeterType",
                table: "ChargeStations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeBoxSerialNumber",
                table: "ChargeStations");

            migrationBuilder.DropColumn(
                name: "ChargePointSerialNumber",
                table: "ChargeStations");

            migrationBuilder.DropColumn(
                name: "MeterSerialNumber",
                table: "ChargeStations");

            migrationBuilder.DropColumn(
                name: "MeterType",
                table: "ChargeStations");

            migrationBuilder.AlterColumn<string>(
                name: "VendorName",
                table: "ChargeStations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "ChargeStations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FirmwareVersion",
                table: "ChargeStations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "ChargeStations",
                type: "TEXT",
                nullable: true);
        }
    }
}
