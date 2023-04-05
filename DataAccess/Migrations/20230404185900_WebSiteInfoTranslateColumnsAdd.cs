using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class WebSiteInfoTranslateColumnsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUsTextDe",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AboutUsTextRu",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(4000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressDe",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressRu",
                schema: "SiteProperty",
                table: "SiteProperty",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUsTextDe",
                schema: "SiteProperty",
                table: "SiteProperty");

            migrationBuilder.DropColumn(
                name: "AboutUsTextRu",
                schema: "SiteProperty",
                table: "SiteProperty");

            migrationBuilder.DropColumn(
                name: "AddressDe",
                schema: "SiteProperty",
                table: "SiteProperty");

            migrationBuilder.DropColumn(
                name: "AddressRu",
                schema: "SiteProperty",
                table: "SiteProperty");
        }
    }
}
