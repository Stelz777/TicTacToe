using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NETCoreTicTacToe.Migrations
{
    public partial class TicTacToeInitialTry14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turns_Histories_HistoryDataTransferObjectId",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Turns");

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryDataTransferObjectId",
                table: "Turns",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_Histories_HistoryDataTransferObjectId",
                table: "Turns",
                column: "HistoryDataTransferObjectId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turns_Histories_HistoryDataTransferObjectId",
                table: "Turns");

            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryDataTransferObjectId",
                table: "Turns",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "HistoryId",
                table: "Turns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_Histories_HistoryDataTransferObjectId",
                table: "Turns",
                column: "HistoryDataTransferObjectId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
