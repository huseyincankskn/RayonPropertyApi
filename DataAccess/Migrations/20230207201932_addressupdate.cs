using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class addressupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TownId",
                schema: "Project",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_CityId",
                schema: "Project",
                table: "Project",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DistrictId",
                schema: "Project",
                table: "Project",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_StreetId",
                schema: "Project",
                table: "Project",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TownId",
                schema: "Project",
                table: "Project",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_City_CityId",
                schema: "Project",
                table: "Project",
                column: "CityId",
                principalSchema: "Address",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_District_DistrictId",
                schema: "Project",
                table: "Project",
                column: "DistrictId",
                principalSchema: "Address",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project",
                column: "StreetId",
                principalSchema: "Address",
                principalTable: "Street",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Town_TownId",
                schema: "Project",
                table: "Project",
                column: "TownId",
                principalSchema: "Address",
                principalTable: "Town",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_City_CityId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_District_DistrictId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Street_StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Town_TownId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_CityId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_DistrictId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TownId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StreetId",
                schema: "Project",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TownId",
                schema: "Project",
                table: "Project");
        }
    }
}
