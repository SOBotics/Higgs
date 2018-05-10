using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Higgs.Server.Data;

namespace Higgs.Server.Migrations
{
    public partial class AddBotOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerAccountId",
                table: "Bots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql($@"UPDATE public.""Bots"" SET ""OwnerAccountId""={DBExtensions.RobAccountId}");

            migrationBuilder.CreateIndex(
                name: "IX_Bots_OwnerAccountId",
                table: "Bots",
                column: "OwnerAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Users_OwnerAccountId",
                table: "Bots",
                column: "OwnerAccountId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Users_OwnerAccountId",
                table: "Bots");

            migrationBuilder.DropIndex(
                name: "IX_Bots_OwnerAccountId",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "OwnerAccountId",
                table: "Bots");
        }
    }
}
