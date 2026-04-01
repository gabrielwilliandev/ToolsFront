using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndToolRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToolTags_Tags_TagsId",
                table: "ToolTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolTags_Tools_ToolsId",
                table: "ToolTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolTags",
                table: "ToolTags");

            migrationBuilder.RenameTable(
                name: "ToolTags",
                newName: "TagTool");

            migrationBuilder.RenameIndex(
                name: "IX_ToolTags_ToolsId",
                table: "TagTool",
                newName: "IX_TagTool_ToolsId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagTool",
                table: "TagTool",
                columns: new[] { "TagsId", "ToolsId" });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tools_UserId",
                table: "Tools",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagTool_Tags_TagsId",
                table: "TagTool",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTool_Tools_ToolsId",
                table: "TagTool",
                column: "ToolsId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Users_UserId",
                table: "Tools",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTool_Tags_TagsId",
                table: "TagTool");

            migrationBuilder.DropForeignKey(
                name: "FK_TagTool_Tools_ToolsId",
                table: "TagTool");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Users_UserId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tools_UserId",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagTool",
                table: "TagTool");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tools");

            migrationBuilder.RenameTable(
                name: "TagTool",
                newName: "ToolTags");

            migrationBuilder.RenameIndex(
                name: "IX_TagTool_ToolsId",
                table: "ToolTags",
                newName: "IX_ToolTags_ToolsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolTags",
                table: "ToolTags",
                columns: new[] { "TagsId", "ToolsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ToolTags_Tags_TagsId",
                table: "ToolTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToolTags_Tools_ToolsId",
                table: "ToolTags",
                column: "ToolsId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
