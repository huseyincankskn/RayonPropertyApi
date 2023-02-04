using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogCategoryAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityCode",
                schema: "Address",
                table: "City",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BlogCategoryId",
                schema: "Blog",
                table: "Blog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
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
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategoryId",
                schema: "Blog",
                table: "Blog",
                column: "BlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategoryId",
                schema: "Blog",
                table: "Blog",
                column: "BlogCategoryId",
                principalSchema: "Blog",
                principalTable: "BlogCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogCategory_BlogCategoryId",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropTable(
                name: "BlogCategory",
                schema: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_BlogCategoryId",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "CityCode",
                schema: "Address",
                table: "City");

            migrationBuilder.DropColumn(
                name: "BlogCategoryId",
                schema: "Blog",
                table: "Blog");
        }
    }
}
