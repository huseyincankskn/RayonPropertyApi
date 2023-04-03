using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogTranslateColumnsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostDe",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostRu",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleDe",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleRu",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(200)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostDe",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "PostRu",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "TitleDe",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "TitleRu",
                schema: "Blog",
                table: "Blog");
        }
    }
}
