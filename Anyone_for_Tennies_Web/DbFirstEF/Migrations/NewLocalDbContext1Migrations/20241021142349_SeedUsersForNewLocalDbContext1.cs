using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbFirstEF.Migrations.NewLocalDbContext1Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersForNewLocalDbContext1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 4, "43/21 New York Ave", new DateTime(1990, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "trump@gmail.com", "Donald", "Trump", "trump", "123456789", "Coach" },
                    { 5, "456 Elm St", new DateTime(1985, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", "Jane", "Smith", "password2", "555-5678", "Coach" },
                    { 6, "789 Oak St", new DateTime(1978, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam@example.com", "Sam", "Wilson", "password3", "555-9101", "Member" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
