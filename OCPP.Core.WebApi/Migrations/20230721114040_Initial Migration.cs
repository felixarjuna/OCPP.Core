using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OCPP.Core.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargeStations",
                columns: table => new
                {
                    StationId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StationName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    VendorName = table.Column<string>(type: "TEXT", nullable: true),
                    FirmwareVersion = table.Column<string>(type: "TEXT", nullable: true),
                    Modem = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientCertThumb = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    Online = table.Column<bool>(type: "INTEGER", nullable: false),
                    Protocol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeStations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "ChargeTags",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TagName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    ParentTagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Blocked = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeKeys", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "MessageLog",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChargePointId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: true),
                    ErrorCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "ConnectorStatuses",
                columns: table => new
                {
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConnectorName = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ChargeRateKW = table.Column<double>(type: "REAL", nullable: true),
                    MeterKWH = table.Column<double>(type: "REAL", nullable: true),
                    SoC = table.Column<double>(type: "REAL", nullable: true),
                    LastStatus = table.Column<double>(type: "REAL", nullable: true),
                    LastStatusTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastMeter = table.Column<double>(type: "REAL", nullable: true),
                    LastMeterTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationId1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorStatuses", x => x.ConnectorId);
                    table.ForeignKey(
                        name: "FK_ConnectorStatuses_ChargeStations_StationId1",
                        column: x => x.StationId1,
                        principalTable: "ChargeStations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uid = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StartTagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeterStart = table.Column<double>(type: "REAL", nullable: false),
                    StartResult = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StopTagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StopTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MeterStop = table.Column<double>(type: "REAL", nullable: true),
                    StopReason = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConnectorId = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ConnectorId1 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_ConnectorStatuses_ConnectorId1",
                        column: x => x.ConnectorId1,
                        principalTable: "ConnectorStatuses",
                        principalColumn: "ConnectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ChargeStation_Identifier",
                table: "ChargeStations",
                column: "StationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConnectorStatuses_StationId1",
                table: "ConnectorStatuses",
                column: "StationId1");

            migrationBuilder.CreateIndex(
                name: "IX_MessageLog_ChargePointId",
                table: "MessageLog",
                column: "LogTime");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ConnectorId1",
                table: "Transactions",
                column: "ConnectorId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeTags");

            migrationBuilder.DropTable(
                name: "MessageLog");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ConnectorStatuses");

            migrationBuilder.DropTable(
                name: "ChargeStations");
        }
    }
}
