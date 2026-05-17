using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantServiceAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPanelAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PanelAccounts",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Login", "PanelType", "PasswordHash" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "admin", "Admin", "gwbklxLIu1mjYNfVsjYjpyAg3klYWmgQjqY2dR2ma1aKsBxNAowel4qrpt+DpE8+" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "waiter", "Waiter", "J2+nogLVA4WpidMG1gpDFO3MeCa/B2z0zwz77y8QW7fX26nUCJmbQzzgeFAJ+F0Q" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "cook", "Cook", "58d3nczSLDy1tNnuUKEZKzBp1PMUuXrUv9WoHGtWE4f3mnaHmSmxBWlApqGfKC8k" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "bartender", "Bartender", "c+MmA7AFqIXx2PLmAYIbyYuq5TkRjKh3z3FNXQEcM/QfpXYm6lGuOF4UHHKdJTjO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PanelAccounts",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "PanelAccounts",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "PanelAccounts",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "PanelAccounts",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
