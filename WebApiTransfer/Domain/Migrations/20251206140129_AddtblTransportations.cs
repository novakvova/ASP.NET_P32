using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddtblTransportations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTransportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    FromCityId = table.Column<int>(type: "integer", nullable: false),
                    ToCityId = table.Column<int>(type: "integer", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeatsTotal = table.Column<int>(type: "integer", nullable: false),
                    SeatsAvailable = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTransportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTransportations_tblCities_FromCityId",
                        column: x => x.FromCityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblTransportations_tblCities_ToCityId",
                        column: x => x.ToCityId,
                        principalTable: "tblCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblTransportations_tblTransportationStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tblTransportationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblTransportations_FromCityId",
                table: "tblTransportations",
                column: "FromCityId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTransportations_StatusId",
                table: "tblTransportations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTransportations_ToCityId",
                table: "tblTransportations",
                column: "ToCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTransportations");
        }
    }
}
