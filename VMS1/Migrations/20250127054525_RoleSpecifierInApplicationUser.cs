using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS1.Migrations
{
    /// <inheritdoc />
    public partial class RoleSpecifierInApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhnNo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoleSpecifier",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleSpecifier",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PhnNo",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
