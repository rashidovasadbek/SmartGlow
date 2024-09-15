using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGlow.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Street_And_OnOffTiming_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StreetName",
                table: "Streets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
                name: "OnOffTimings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OnTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OffTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OnLights = table.Column<byte>(type: "smallint", nullable: false),
                    OffLights = table.Column<byte>(type: "smallint", nullable: false),
                    LitUnits = table.Column<byte>(type: "smallint", nullable: false),
                    StreetId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnOffTimings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnOffTimings_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnOffTimings_StreetId",
                table: "OnOffTimings",
                column: "StreetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnOffTimings");

            migrationBuilder.AlterColumn<string>(
                name: "StreetName",
                table: "Streets",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
