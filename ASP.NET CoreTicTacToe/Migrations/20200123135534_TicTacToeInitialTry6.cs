using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NETCoreTicTacToe.Migrations
{
    public partial class TicTacToeInitialTry6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Проверить аргументы или открытые методы
            migrationBuilder.CreateTable(
#pragma warning restore CA1062 // Проверить аргументы или открытые методы
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SerializedSquares = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryId = table.Column<Guid>(nullable: true),
                    BoardId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Games_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HistoryId = table.Column<Guid>(nullable: false),
                    CellNumber = table.Column<int>(nullable: false),
                    WhichTurn = table.Column<int>(nullable: false),
                    HistoryDataTransferObjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turns_Histories_HistoryDataTransferObjectId",
                        column: x => x.HistoryDataTransferObjectId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_BoardId",
                table: "Games",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HistoryId",
                table: "Games",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_HistoryDataTransferObjectId",
                table: "Turns",
                column: "HistoryDataTransferObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
#pragma warning disable CA1062 // Проверить аргументы или открытые методы
            migrationBuilder.DropTable(
#pragma warning restore CA1062 // Проверить аргументы или открытые методы
                name: "Games");

            migrationBuilder.DropTable(
                name: "Turns");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}
