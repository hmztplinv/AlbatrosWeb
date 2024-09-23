using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbatrosPortfoyPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuFieldsToPages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInMenu",
                table: "Pages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Pages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MenuPosition",
                table: "Pages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInMenu",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "MenuPosition",
                table: "Pages");
        }
    }
}
