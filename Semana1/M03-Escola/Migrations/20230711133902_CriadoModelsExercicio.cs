using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M03_Escola.Migrations
{
    /// <inheritdoc />
    public partial class CriadoModelsExercicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "AlunoTB",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AlunoTurma",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Aluno = table.Column<int>(type: "int", nullable: false),
                    FK_Turma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoTurma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_AlunoTB_FK_Aluno",
                        column: x => x.FK_Aluno,
                        principalTable: "AlunoTB",
                        principalColumn: "PK_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoTurma_TURMA_FK_Turma",
                        column: x => x.FK_Turma,
                        principalTable: "TURMA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BOLETIM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DATA = table.Column<DateTime>(type: "date", nullable: false),
                    FK_Aluno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOLETIM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BOLETIM_AlunoTB_FK_Aluno",
                        column: x => x.FK_Aluno,
                        principalTable: "AlunoTB",
                        principalColumn: "PK_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotasMateria",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_Boletim = table.Column<int>(type: "int", nullable: false),
                    FK_Materia = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasMateria", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotasMateria_BOLETIM_FK_Boletim",
                        column: x => x.FK_Boletim,
                        principalTable: "BOLETIM",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasMateria_Materia_FK_Materia",
                        column: x => x.FK_Materia,
                        principalTable: "Materia",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_FK_Aluno",
                table: "AlunoTurma",
                column: "FK_Aluno");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTurma_FK_Turma",
                table: "AlunoTurma",
                column: "FK_Turma");

            migrationBuilder.CreateIndex(
                name: "IX_BOLETIM_FK_Aluno",
                table: "BOLETIM",
                column: "FK_Aluno");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_Nome",
                table: "Materia",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateria_FK_Boletim",
                table: "NotasMateria",
                column: "FK_Boletim");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMateria_FK_Materia",
                table: "NotasMateria",
                column: "FK_Materia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTurma");

            migrationBuilder.DropTable(
                name: "NotasMateria");

            migrationBuilder.DropTable(
                name: "BOLETIM");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropColumn(
                name: "Idade",
                table: "AlunoTB");
        }
    }
}
