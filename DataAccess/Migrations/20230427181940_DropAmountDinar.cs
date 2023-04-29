using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class DropAmountDinar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDinar",
                schema: "Project",
                table: "Project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceDinar",
                schema: "Project",
                table: "Project",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
