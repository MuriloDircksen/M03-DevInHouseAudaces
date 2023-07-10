using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M03_Escola.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlunoTB",
                columns: table => new
                {
                    PK_ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    SOBRENOME = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    GENERO = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    DATA_NASCIMENTO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_aluno_id", x => x.PK_ID);
                });

            migrationBuilder.CreateTable(
                name: "TURMA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CURSO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "Curso Basico"),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TURMA", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoTB_EMAIL",
                table: "AlunoTB",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TURMA_Nome",
                table: "TURMA",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoTB");

            migrationBuilder.DropTable(
                name: "TURMA");
        }
    }
}
