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
                name: "ChargePoint",
                columns: table => new
                {
                    ChargeStationId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ClientCertThumb = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargePoint", x => x.ChargeStationId);
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
                name: "ConnectorStatus",
                columns: table => new
                {
                    ChargePointId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastStatus = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastStatusTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastMeter = table.Column<double>(type: "REAL", nullable: true),
                    LastMeterTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectorStatus", x => new { x.ChargePointId, x.ConnectorId });
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
                    ErrorCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLog", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Uid = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ChargePointId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ConnectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeterStart = table.Column<double>(type: "REAL", nullable: false),
                    StartResult = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StopTagId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    StopTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MeterStop = table.Column<double>(type: "REAL", nullable: true),
                    StopReason = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_ChargePoint",
                        column: x => x.ChargePointId,
                        principalTable: "ChargePoint",
                        principalColumn: "ChargeStationId");
                });

            migrationBuilder.CreateIndex(
                name: "ChargePoint_Identifier",
                table: "ChargePoint",
                column: "ChargeStationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageLog_ChargePointId",
                table: "MessageLog",
                column: "LogTime");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ChargePointId",
                table: "Transactions",
                column: "ChargePointId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargeTags");

            migrationBuilder.DropTable(
                name: "ConnectorStatus");

            migrationBuilder.DropTable(
                name: "MessageLog");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "ChargePoint");
        }
    }
}
