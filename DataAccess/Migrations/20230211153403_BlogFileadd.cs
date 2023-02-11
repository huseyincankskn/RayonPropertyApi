using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogFileadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BlogFileId",
                schema: "Blog",
                table: "Blog",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BlogFile",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    FileName = table.Column<string>(type: "nvarchar(200)", nullable: false),
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
                    table.PrimaryKey("PK_BlogFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogFileId",
                schema: "Blog",
                table: "Blog",
                column: "BlogFileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BlogFile_BlogFileId",
                schema: "Blog",
                table: "Blog",
                column: "BlogFileId",
                principalSchema: "Blog",
                principalTable: "BlogFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BlogFile_BlogFileId",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropTable(
                name: "BlogFile",
                schema: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_BlogFileId",
                schema: "Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "BlogFileId",
                schema: "Blog",
                table: "Blog");
        }
    }
}
