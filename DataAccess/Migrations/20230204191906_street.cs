using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class street : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Town_TownId",
                schema: "Address",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Town_City_CityId",
                schema: "Address",
                table: "Town");

            migrationBuilder.CreateTable(
                name: "Street",
                schema: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Pk_id = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Street", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Street_District_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "Address",
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Street_DistrictId",
                schema: "Address",
                table: "Street",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Town_TownId",
                schema: "Address",
                table: "District",
                column: "TownId",
                principalSchema: "Address",
                principalTable: "Town",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Town_City_CityId",
                schema: "Address",
                table: "Town",
                column: "CityId",
                principalSchema: "Address",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Town_TownId",
                schema: "Address",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Town_City_CityId",
                schema: "Address",
                table: "Town");

            migrationBuilder.DropTable(
                name: "Street",
                schema: "Address");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Town_TownId",
                schema: "Address",
                table: "District",
                column: "TownId",
                principalSchema: "Address",
                principalTable: "Town",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Town_City_CityId",
                schema: "Address",
                table: "Town",
                column: "CityId",
                principalSchema: "Address",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
