using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tools.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddListas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Users_UserId",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tools",
                newName: "ListaId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_UserId",
                table: "Tools",
                newName: "IX_Tools_ListaId");

            migrationBuilder.CreateTable(
                name: "Listas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listas_UserId",
                table: "Listas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Listas_ListaId",
                table: "Tools",
                column: "ListaId",
                principalTable: "Listas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Listas_ListaId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "Listas");

            migrationBuilder.RenameColumn(
                name: "ListaId",
                table: "Tools",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_ListaId",
                table: "Tools",
                newName: "IX_Tools_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Users_UserId",
                table: "Tools",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
