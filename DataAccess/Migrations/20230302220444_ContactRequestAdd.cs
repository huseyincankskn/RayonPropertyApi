using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ContactRequestAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Contact");

            migrationBuilder.CreateTable(
                name: "ContactRequest",
                schema: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    AddDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    AddUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRequest",
                schema: "Contact");
        }
    }
}
