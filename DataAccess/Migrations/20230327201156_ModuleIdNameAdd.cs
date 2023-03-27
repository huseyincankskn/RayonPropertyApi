using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ModuleIdNameAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ModuleId",
                schema: "Auth",
                table: "Role",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "ModuleName",
                schema: "Auth",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModuleId",
                schema: "Auth",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "ModuleName",
                schema: "Auth",
                table: "Role");
        }
    }
}
