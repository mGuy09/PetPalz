using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetPalz.Migrations
{
    /// <inheritdoc />
    public partial class userReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7a1b8d7-aeb6-43ce-958f-ab226226ab12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc32276e-a516-4a03-95ad-bf57dc433007");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ec5a3bee-3b8d-41ce-bcb7-95f796c3162d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "043dbcdf-6595-4034-b67d-b4be53fee0dd", "1", "Admin", "ADMIN" },
                    { "05e1f61e-e090-4e9c-8c38-36cda0b1b222", "2", "petOwner", "PETOWNER" },
                    { "4c7c8aee-c0b9-4cb4-b4dd-73617676dc84", "3", "petSitter", "PETSITTER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "043dbcdf-6595-4034-b67d-b4be53fee0dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05e1f61e-e090-4e9c-8c38-36cda0b1b222");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c7c8aee-c0b9-4cb4-b4dd-73617676dc84");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7a1b8d7-aeb6-43ce-958f-ab226226ab12", "1", "Admin", "ADMIN" },
                    { "bc32276e-a516-4a03-95ad-bf57dc433007", "2", "petOwner", "PETOWNER" },
                    { "ec5a3bee-3b8d-41ce-bcb7-95f796c3162d", "3", "petSitter", "PETSITTER" }
                });
        }
    }
}
