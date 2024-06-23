using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sandesh_SMS.Migrations
{
    /// <inheritdoc />
    public partial class UYHGUHKFHUK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRoleId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "AspNetUsers");
        }
    }
}
