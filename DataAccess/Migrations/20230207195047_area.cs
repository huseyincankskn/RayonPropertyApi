using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class area : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                schema: "Project",
                table: "Project");

            migrationBuilder.AlterColumn<short>(
                name: "SeeClose",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<short>(
                name: "GrossArea",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "NetArea",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossArea",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "NetArea",
                schema: "Project",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "SeeClose",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
