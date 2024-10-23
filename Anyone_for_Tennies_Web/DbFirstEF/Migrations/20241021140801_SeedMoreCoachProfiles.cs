using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbFirstEF.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreCoachProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CoachProfiles",
                columns: new[] { "CoachId", "Biography" },
                values: new object[,]
                {
                    { 4, "Biography for Coach 4" },
                    { 5, "Biography for Coach 5" },
                    { 6, "Biography for Coach 6" },
                    { 7, "Biography for Coach 7" },
                    { 8, "Biography for Coach 8" },
                    { 9, "Biography for Coach 9" },
                    { 10, "Biography for Coach 10" },
                    { 11, "Biography for Coach 11" },
                    { 12, "Biography for Coach 12" },
                    { 13, "Biography for Coach 13" },
                    { 14, "Biography for Coach 14" },
                    { 15, "Biography for Coach 15" },
                    { 16, "Biography for Coach 16" },
                    { 17, "Biography for Coach 17" },
                    { 18, "Biography for Coach 18" },
                    { 19, "Biography for Coach 19" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "CoachProfiles",
                keyColumn: "CoachId",
                keyValue: 19);
        }
    }
}
