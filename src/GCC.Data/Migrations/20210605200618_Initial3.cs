
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GCC.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Exame_ExameId",
                table: "Consulta");

            migrationBuilder.DropIndex(
                name: "IX_Consulta_ExameId",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "ExameId",
                table: "Consulta");

            migrationBuilder.CreateTable(
                name: "ConsultaExame",
                columns: table => new
                {
                    ConsultaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaExame", x => new { x.ConsultaId, x.ExameId });
                    table.ForeignKey(
                        name: "FK_ConsultaExame_Consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consulta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultaExame_Exame_ExameId",
                        column: x => x.ExameId,
                        principalTable: "Exame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaExame_ExameId",
                table: "ConsultaExame",
                column: "ExameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultaExame");

            migrationBuilder.AddColumn<Guid>(
                name: "ExameId",
                table: "Consulta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_ExameId",
                table: "Consulta",
                column: "ExameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Exame_ExameId",
                table: "Consulta",
                column: "ExameId",
                principalTable: "Exame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
