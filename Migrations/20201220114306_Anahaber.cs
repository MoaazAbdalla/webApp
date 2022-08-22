using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCreate.Migrations
{
    public partial class Anahaber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_anahaber",
                table: "anahaber");

            migrationBuilder.RenameTable(
                name: "anahaber",
                newName: "anahaberleri");

            migrationBuilder.AddPrimaryKey(
                name: "PK_anahaberleri",
                table: "anahaberleri",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_anahaberleri",
                table: "anahaberleri");

            migrationBuilder.RenameTable(
                name: "anahaberleri",
                newName: "anahaber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_anahaber",
                table: "anahaber",
                column: "Id");
        }
    }
}
