using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NETCoreTicTacToe.Migrations
{
    public partial class Initialv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "RealPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Side = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealPlayers", x => x.Id);
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
                name: "FK_Games_RealPlayers_TacPlayerId",
                table: "Games",
                column: "TacPlayerId",
                principalTable: "RealPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_RealPlayers_TicPlayerId",
                table: "Games",
                column: "TicPlayerId",
                principalTable: "RealPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_RealPlayers_TacPlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_RealPlayers_TicPlayerId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "RealPlayers");

            migrationBuilder.DropIndex(
                name: "IX_Games_TacPlayerId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TicPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TacPlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TicPlayerId",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "SerializedSquares",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
