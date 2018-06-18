using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class RenameBotsToDashboards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Bots_BotId",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConflictExceptions_Bots_BotId",
                table: "ConflictExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Bots_BotId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Bots_BotId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "BotId",
                table: "Reports",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_BotId",
                table: "Reports",
                newName: "IX_Reports_DashboardId");

            migrationBuilder.RenameColumn(
                name: "BotId",
                table: "Feedbacks",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_BotId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_DashboardId");

            migrationBuilder.RenameColumn(
                name: "BotId",
                table: "ConflictExceptions",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_ConflictExceptions_BotId",
                table: "ConflictExceptions",
                newName: "IX_ConflictExceptions_DashboardId");

            migrationBuilder.RenameColumn(
                name: "BotId",
                table: "BotScopes",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_BotId",
                table: "BotScopes",
                newName: "IX_BotScopes_DashboardId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Bots",
                newName: "BotName");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredFeedback",
                table: "ConflictExceptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Bots_DashboardId",
                table: "BotScopes",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
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
                name: "FK_Reports_Bots_DashboardId",
                table: "Reports",
                column: "DashboardId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Bots_DashboardId",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ConflictExceptions_Bots_DashboardId",
                table: "ConflictExceptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Bots_DashboardId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Bots_DashboardId",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "Reports",
                newName: "BotId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_DashboardId",
                table: "Reports",
                newName: "IX_Reports_BotId");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "Feedbacks",
                newName: "BotId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_DashboardId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_BotId");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "ConflictExceptions",
                newName: "BotId");

            migrationBuilder.RenameIndex(
                name: "IX_ConflictExceptions_DashboardId",
                table: "ConflictExceptions",
                newName: "IX_ConflictExceptions_BotId");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "BotScopes",
                newName: "BotId");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_DashboardId",
                table: "BotScopes",
                newName: "IX_BotScopes_BotId");

            migrationBuilder.RenameColumn(
                name: "BotName",
                table: "Bots",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredFeedback",
                table: "ConflictExceptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Bots_BotId",
                table: "BotScopes",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConflictExceptions_Bots_BotId",
                table: "ConflictExceptions",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Bots_BotId",
                table: "Feedbacks",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Bots_BotId",
                table: "Reports",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
