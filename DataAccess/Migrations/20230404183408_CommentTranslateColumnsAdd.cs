using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class CommentTranslateColumnsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentTextDe",
                schema: "Comment",
                table: "Comment",
                type: "nvarchar(3000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommentTextRu",
                schema: "Comment",
                table: "Comment",
                type: "nvarchar(3000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentTextDe",
                schema: "Comment",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentTextRu",
                schema: "Comment",
                table: "Comment");
        }
    }
}
