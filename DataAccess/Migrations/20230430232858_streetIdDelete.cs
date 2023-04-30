using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class streetIdDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project",
                column: "StreetId",
                principalSchema: "Address",
                principalTable: "Street",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
