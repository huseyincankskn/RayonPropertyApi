using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class nullable_streetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project",
                column: "StreetId",
                principalSchema: "Address",
                principalTable: "Street",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "StreetId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project",
                column: "StreetId",
                principalSchema: "Address",
                principalTable: "Street",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
