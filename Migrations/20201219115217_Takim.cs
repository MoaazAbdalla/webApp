using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCreate.Migrations
{
    public partial class Takim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "teammembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "instagram",
                table: "teammembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "twitter",
                table: "teammembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "whatsapp",
                table: "teammembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "teammembers");

            migrationBuilder.DropColumn(
                name: "instagram",
                table: "teammembers");

            migrationBuilder.DropColumn(
                name: "twitter",
                table: "teammembers");

            migrationBuilder.DropColumn(
                name: "whatsapp",
                table: "teammembers");
        }
    }
}
