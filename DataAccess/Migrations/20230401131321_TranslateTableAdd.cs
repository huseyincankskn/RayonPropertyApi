using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class TranslateTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Translate");

            migrationBuilder.CreateTable(
                name: "Translate",
                schema: "Translate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Key = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    KeyDe = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    KeyRu = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    IsWritten = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Translate", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translate",
                schema: "Translate");
        }
    }
}
