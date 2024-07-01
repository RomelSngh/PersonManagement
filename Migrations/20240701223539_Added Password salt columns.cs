using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedPasswordsaltcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passwordsalt",
                table: "User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "passwordsalt",
                table: "Tempuser",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordsalt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "passwordsalt",
                table: "Tempuser");
        }
    }
}
