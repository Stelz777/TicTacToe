using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NETCoreTicTacToe.Migrations
{
    public partial class Initialv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhichTurn",
                table: "Turns");

            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "Turns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TacPlayerId",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicPlayerId",
                table: "Games",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerializedSquares",
                table: "Boards",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Side = table.Column<int>(nullable: false),
                    IsBot = table.Column<bool>(nullable: false),
                    Difficulty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_TacPlayerId",
                table: "Games",
                column: "TacPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TicPlayerId",
                table: "Games",
                column: "TicPlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_TacPlayerId",
                table: "Games",
                column: "TacPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_TicPlayerId",
                table: "Games",
                column: "TicPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_TacPlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_TicPlayerId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Games_TacPlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TicPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "TacPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TicPlayerId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "WhichTurn",
                table: "Turns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SerializedSquares",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
