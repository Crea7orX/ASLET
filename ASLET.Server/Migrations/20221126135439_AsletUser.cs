using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASLET.Server.Migrations
{
    /// <inheritdoc />
    public partial class AsletUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fistname",
                table: "AsletUsers",
                newName: "Firstname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "AsletUsers",
                newName: "Fistname");
        }
    }
}
