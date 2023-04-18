using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperNews.Migrations
{
    public partial class AddViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "News");
        }
    }
}
