using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SaloonCountString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedCount",
                schema: "Project",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "SaloonCount",
                schema: "Project",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "SaloonCount",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<short>(
                name: "BedCount",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
