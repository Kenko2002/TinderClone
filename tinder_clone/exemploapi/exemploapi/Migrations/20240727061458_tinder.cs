using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exemploapi.Migrations
{
    /// <inheritdoc />
    public partial class tinder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exemplo_paiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examples_Examples_exemplo_paiId",
                        column: x => x.exemplo_paiId,
                        principalTable: "Examples",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latitude = table.Column<float>(type: "real", nullable: false),
                    longitude = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilegios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isPremium = table.Column<bool>(type: "bit", nullable: false),
                    isFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilegios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    privilegiosId = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    descricaoPessoal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    localizacaoId = table.Column<int>(type: "int", nullable: true),
                    generoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Generos_generoId",
                        column: x => x.generoId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Localizacoes_localizacaoId",
                        column: x => x.localizacaoId,
                        principalTable: "Localizacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Privilegios_privilegiosId",
                        column: x => x.privilegiosId,
                        principalTable: "Privilegios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Atracoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    generoId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atracoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atracoes_Generos_generoId",
                        column: x => x.generoId,
                        principalTable: "Generos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atracoes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_requirinte_id = table.Column<int>(type: "int", nullable: true),
                    user_requirido_id = table.Column<int>(type: "int", nullable: true),
                    Criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchAceito = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atracoes_generoId",
                table: "Atracoes",
                column: "generoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atracoes_UserId",
                table: "Atracoes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_exemplo_paiId",
                table: "Examples",
                column: "exemplo_paiId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_generoId",
                table: "Users",
                column: "generoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_localizacaoId",
                table: "Users",
                column: "localizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_privilegiosId",
                table: "Users",
                column: "privilegiosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atracoes");

            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropTable(
                name: "Privilegios");
        }
    }
}
