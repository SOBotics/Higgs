using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class MarkBotAndUserScopeNamesRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScopes_Scopes_ScopeName",
                table: "UserScopes");

            migrationBuilder.AlterColumn<string>(
                name: "ScopeName",
                table: "UserScopes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ScopeName",
                table: "BotScopes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScopes_Scopes_ScopeName",
                table: "UserScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserScopes_Scopes_ScopeName",
                table: "UserScopes");

            migrationBuilder.AlterColumn<string>(
                name: "ScopeName",
                table: "UserScopes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ScopeName",
                table: "BotScopes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserScopes_Scopes_ScopeName",
                table: "UserScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
