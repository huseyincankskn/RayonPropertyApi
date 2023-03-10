using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class projectUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCount",
                schema: "Project",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "IsEmpty",
                schema: "Project",
                table: "Project",
                newName: "IsSold");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectDate",
                schema: "Project",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectDate",
                schema: "Project",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "IsSold",
                schema: "Project",
                table: "Project",
                newName: "IsEmpty");

            migrationBuilder.AddColumn<short>(
                name: "RoomCount",
                schema: "Project",
                table: "Project",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
