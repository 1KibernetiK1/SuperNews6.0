using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperNews.Migrations
{
    public partial class likesAnddislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "News");
        }
    }
}
