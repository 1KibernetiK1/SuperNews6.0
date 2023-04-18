using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperNews.Migrations
{
    public partial class Editsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaberId",
                table: "Comments");

            migrationBuilder.AddColumn<long>(
                name: "NewsId",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "HaberId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
