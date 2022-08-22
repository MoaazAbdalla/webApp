using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCreate.Migrations
{
    public partial class galal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img",
                table: "news",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "news");
        }
    }
}
