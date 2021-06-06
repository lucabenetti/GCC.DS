using Microsoft.EntityFrameworkCore.Migrations;

namespace GCC.Data.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Medico_MedicoId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Paciente_PacienteId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaExame_Consulta_ConsultaId",
                table: "ConsultaExame");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaExame_Exame_ExameId",
                table: "ConsultaExame");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Medico_MedicoId",
                table: "Consulta",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Paciente_PacienteId",
                table: "Consulta",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaExame_Consulta_ConsultaId",
                table: "ConsultaExame",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaExame_Exame_ExameId",
                table: "ConsultaExame",
                column: "ExameId",
                principalTable: "Exame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Medico_MedicoId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Paciente_PacienteId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaExame_Consulta_ConsultaId",
                table: "ConsultaExame");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultaExame_Exame_ExameId",
                table: "ConsultaExame");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Medico_MedicoId",
                table: "Consulta",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Paciente_PacienteId",
                table: "Consulta",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaExame_Consulta_ConsultaId",
                table: "ConsultaExame",
                column: "ConsultaId",
                principalTable: "Consulta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultaExame_Exame_ExameId",
                table: "ConsultaExame",
                column: "ExameId",
                principalTable: "Exame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
