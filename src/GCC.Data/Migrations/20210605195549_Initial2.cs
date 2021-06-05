using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GCC.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExameId",
                table: "Consulta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Consulta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receita",
                table: "Consulta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exame",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exame", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Exame_ExameId",
                table: "Consulta");

            migrationBuilder.DropTable(
                name: "Exame");

            migrationBuilder.DropIndex(
                name: "IX_Consulta_ExameId",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "ExameId",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Receita",
                table: "Consulta");
        }
    }
}
