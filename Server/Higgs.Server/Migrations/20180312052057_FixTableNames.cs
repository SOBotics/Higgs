using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class FixTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbBotScope_Bots_BotId",
                table: "DbBotScope");

            migrationBuilder.DropForeignKey(
                name: "FK_DbBotScope_Scopes_ScopeName",
                table: "DbBotScope");

            migrationBuilder.DropForeignKey(
                name: "FK_DbReportFeedback_Feedbacks_FeedbackId",
                table: "DbReportFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_DbReportFeedback_Reports_ReportId",
                table: "DbReportFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_DbReportFeedback_Users_UserId",
                table: "DbReportFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbReportFeedback",
                table: "DbReportFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DbBotScope",
                table: "DbBotScope");

            migrationBuilder.RenameTable(
                name: "DbReportFeedback",
                newName: "ReportFeedbacks");

            migrationBuilder.RenameTable(
                name: "DbBotScope",
                newName: "BotScopes");

            migrationBuilder.RenameIndex(
                name: "IX_DbReportFeedback_UserId",
                table: "ReportFeedbacks",
                newName: "IX_ReportFeedbacks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DbReportFeedback_ReportId",
                table: "ReportFeedbacks",
                newName: "IX_ReportFeedbacks_ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_DbReportFeedback_FeedbackId",
                table: "ReportFeedbacks",
                newName: "IX_ReportFeedbacks_FeedbackId");

            migrationBuilder.RenameIndex(
                name: "IX_DbBotScope_ScopeName",
                table: "BotScopes",
                newName: "IX_BotScopes_ScopeName");

            migrationBuilder.RenameIndex(
                name: "IX_DbBotScope_BotId",
                table: "BotScopes",
                newName: "IX_BotScopes_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReportFeedbacks",
                table: "ReportFeedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BotScopes",
                table: "BotScopes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Bots_BotId",
                table: "BotScopes",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFeedbacks_Feedbacks_FeedbackId",
                table: "ReportFeedbacks",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFeedbacks_Reports_ReportId",
                table: "ReportFeedbacks",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFeedbacks_Users_UserId",
                table: "ReportFeedbacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Bots_BotId",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_BotScopes_Scopes_ScopeName",
                table: "BotScopes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportFeedbacks_Feedbacks_FeedbackId",
                table: "ReportFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportFeedbacks_Reports_ReportId",
                table: "ReportFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_ReportFeedbacks_Users_UserId",
                table: "ReportFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReportFeedbacks",
                table: "ReportFeedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BotScopes",
                table: "BotScopes");

            migrationBuilder.RenameTable(
                name: "ReportFeedbacks",
                newName: "DbReportFeedback");

            migrationBuilder.RenameTable(
                name: "BotScopes",
                newName: "DbBotScope");

            migrationBuilder.RenameIndex(
                name: "IX_ReportFeedbacks_UserId",
                table: "DbReportFeedback",
                newName: "IX_DbReportFeedback_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportFeedbacks_ReportId",
                table: "DbReportFeedback",
                newName: "IX_DbReportFeedback_ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_ReportFeedbacks_FeedbackId",
                table: "DbReportFeedback",
                newName: "IX_DbReportFeedback_FeedbackId");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_ScopeName",
                table: "DbBotScope",
                newName: "IX_DbBotScope_ScopeName");

            migrationBuilder.RenameIndex(
                name: "IX_BotScopes_BotId",
                table: "DbBotScope",
                newName: "IX_DbBotScope_BotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbReportFeedback",
                table: "DbReportFeedback",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbBotScope",
                table: "DbBotScope",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DbBotScope_Bots_BotId",
                table: "DbBotScope",
                column: "BotId",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DbBotScope_Scopes_ScopeName",
                table: "DbBotScope",
                column: "ScopeName",
                principalTable: "Scopes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DbReportFeedback_Feedbacks_FeedbackId",
                table: "DbReportFeedback",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DbReportFeedback_Reports_ReportId",
                table: "DbReportFeedback",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DbReportFeedback_Users_UserId",
                table: "DbReportFeedback",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
