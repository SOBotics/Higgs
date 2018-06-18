using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class RenameBotsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bots_Users_OwnerAccountId",
                table: "Bots");

            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Bots_DashboardId",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConflictExceptions_Bots_DashboardId",
                table: "ConflictExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Bots_DashboardId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Bots_BotId",
                table: "Reasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Bots_DashboardId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotScopes",
                table: "BotScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bots",
                table: "Bots");

            migrationBuilder.RenameTable(
                name: "BotScopes",
                newName: "DashboardScopes");

            migrationBuilder.RenameTable(
                name: "Bots",
                newName: "Dashboards");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_ScopeName",
                table: "DashboardScopes",
                newName: "IX_DashboardScopes_ScopeName");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_DashboardId",
                table: "DashboardScopes",
                newName: "IX_DashboardScopes_DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Bots_OwnerAccountId",
                table: "Dashboards",
                newName: "IX_Dashboards_OwnerAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DashboardScopes",
                table: "DashboardScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dashboards",
                table: "Dashboards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConflictExceptions_Dashboards_DashboardId",
                table: "ConflictExceptions",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Users_OwnerAccountId",
                table: "Dashboards",
                column: "OwnerAccountId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardScopes_Dashboards_DashboardId",
                table: "DashboardScopes",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardScopes_Scopes_ScopeName",
                table: "DashboardScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Dashboards_DashboardId",
                table: "Feedbacks",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Dashboards_BotId",
                table: "Reasons",
                column: "BotId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Dashboards_DashboardId",
                table: "Reports",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConflictExceptions_Dashboards_DashboardId",
                table: "ConflictExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Users_OwnerAccountId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardScopes_Dashboards_DashboardId",
                table: "DashboardScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardScopes_Scopes_ScopeName",
                table: "DashboardScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Dashboards_DashboardId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Dashboards_BotId",
                table: "Reasons");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Dashboards_DashboardId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DashboardScopes",
                table: "DashboardScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dashboards",
                table: "Dashboards");

            migrationBuilder.RenameTable(
                name: "DashboardScopes",
                newName: "BotScopes");

            migrationBuilder.RenameTable(
                name: "Dashboards",
                newName: "Bots");

            migrationBuilder.RenameIndex(
                name: "IX_DashboardScopes_ScopeName",
                table: "BotScopes",
                newName: "IX_BotScopes_ScopeName");

            migrationBuilder.RenameIndex(
                name: "IX_DashboardScopes_DashboardId",
                table: "BotScopes",
                newName: "IX_BotScopes_DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Dashboards_OwnerAccountId",
                table: "Bots",
                newName: "IX_Bots_OwnerAccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotScopes",
                table: "BotScopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bots",
                table: "Bots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bots_Users_OwnerAccountId",
                table: "Bots",
                column: "OwnerAccountId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Bots_DashboardId",
                table: "BotScopes",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConflictExceptions_Bots_DashboardId",
                table: "ConflictExceptions",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Bots_DashboardId",
                table: "Feedbacks",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Bots_BotId",
                table: "Reasons",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Bots_DashboardId",
                table: "Reports",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
