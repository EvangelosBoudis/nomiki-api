using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nomiki.Api.InterestRate.Database.Migrations
{
    /// <inheritdoc />
    public partial class InterestRateDefinitionUniqueFromTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_interest_rate_definition_from_to",
                table: "interest_rate_definition");

            migrationBuilder.CreateIndex(
                name: "ix_interest_rate_definition_from_to",
                table: "interest_rate_definition",
                columns: new[] { "from", "to" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_interest_rate_definition_from_to",
                table: "interest_rate_definition");

            migrationBuilder.CreateIndex(
                name: "ix_interest_rate_definition_from_to",
                table: "interest_rate_definition",
                columns: new[] { "from", "to" });
        }
    }
}
