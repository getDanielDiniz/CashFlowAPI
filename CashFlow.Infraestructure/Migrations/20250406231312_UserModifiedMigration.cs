using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UserModifiedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "Users",
                newName: "UserIdentifier");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserIdentifier",
                table: "Users",
                newName: "Token");
        }
    }
}
