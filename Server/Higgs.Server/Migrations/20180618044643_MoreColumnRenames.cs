using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class MoreColumnRenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Dashboards_BotId",
                table: "Reasons");

            migrationBuilder.RenameColumn(
                name: "BotId",
                table: "Reasons",
                newName: "DashboardId");

            migrationBuilder.RenameIndex(
                name: "IX_Reasons_BotId",
                table: "Reasons",
                newName: "IX_Reasons_DashboardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Dashboards_DashboardId",
                table: "Reasons",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reasons_Dashboards_DashboardId",
                table: "Reasons");

            migrationBuilder.RenameColumn(
                name: "DashboardId",
                table: "Reasons",
                newName: "BotId");

            migrationBuilder.RenameIndex(
                name: "IX_Reasons_DashboardId",
                table: "Reasons",
                newName: "IX_Reasons_BotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reasons_Dashboards_BotId",
                table: "Reasons",
                column: "BotId",
                principalTable: "Dashboards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
