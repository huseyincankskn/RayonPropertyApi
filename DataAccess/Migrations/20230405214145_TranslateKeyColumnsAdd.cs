using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class TranslateKeyColumnsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranslateKey",
                schema: "Translate",
                table: "Translate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutUsTranslateKey",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressTranslateKey",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDe",
                table: "ProjectFeature",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "ProjectFeature",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTranslateKey",
                table: "ProjectFeature",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionDe",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRu",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTranslateKey",
                schema: "Project",
                table: "Project",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleDe",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleRu",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleTranslateKey",
                schema: "Project",
                table: "Project",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommentTextTranslateKey",
                schema: "Comment",
                table: "Comment",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTranslateKey",
                schema: "Blog",
                table: "BlogCategory",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostTranslateKey",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleTranslateKey",
                schema: "Blog",
                table: "Blog",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslateKey",
                schema: "Translate",
                table: "Translate");

            migrationBuilder.DropColumn(
                name: "AboutUsTranslateKey",
                schema: "SiteProperty",
                table: "SiteProperty");

            migrationBuilder.DropColumn(
                name: "AddressTranslateKey",
                schema: "SiteProperty",
                table: "SiteProperty");

            migrationBuilder.DropColumn(
                name: "NameDe",
                table: "ProjectFeature");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "ProjectFeature");

            migrationBuilder.DropColumn(
                name: "NameTranslateKey",
                table: "ProjectFeature");

            migrationBuilder.DropColumn(
                name: "DescriptionDe",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DescriptionRu",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DescriptionTranslateKey",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TitleDe",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TitleRu",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TitleTranslateKey",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CommentTextTranslateKey",
                schema: "Comment",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "NameTranslateKey",
                schema: "Blog",
                table: "BlogCategory");

            migrationBuilder.DropColumn(
                name: "PostTranslateKey",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "TitleTranslateKey",
                schema: "Blog",
                table: "Blog");
        }
    }
}
