using Microsoft.EntityFrameworkCore.Migrations;

namespace JevoCrypt.Migrations
{
    public partial class keepsignin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KeepSignIn",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeepSignIn",
                table: "Users");
        }
    }
}
