using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetPalz.Migrations
{
    /// <inheritdoc />
    public partial class added_new_col_for_servicetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForOwner",
                table: "ServiceTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsForOwner",
                table: "ServiceTypes");
        }
    }
}
