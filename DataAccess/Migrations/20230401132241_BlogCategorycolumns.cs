using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogCategorycolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameDe",
                schema: "Blog",
                table: "BlogCategory",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                schema: "Blog",
                table: "BlogCategory",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameDe",
                schema: "Blog",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "NameRu",
                schema: "Blog",
                table: "BlogCategory");
        }
    }
}
