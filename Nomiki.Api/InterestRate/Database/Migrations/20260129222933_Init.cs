using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nomiki.Api.InterestRate.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interest_rate_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from = table.Column<DateOnly>(type: "date", nullable: false),
                    to = table.Column<DateOnly>(type: "date", nullable: true),
                    administrative_act = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    fek = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contractual_rate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    default_rate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    deterministic_hash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interest_rate_definition", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_interest_rate_definition_from_to",
                table: "interest_rate_definition",
                columns: new[] { "from", "to" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interest_rate_definition");
        }
    }
}
