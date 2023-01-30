using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogPostUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Post",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Post",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(5000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");
        }
    }
}
