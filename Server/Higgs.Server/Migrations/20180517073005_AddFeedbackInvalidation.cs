using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class AddFeedbackInvalidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvalidatedByUserId",
                table: "ReportFeedbacks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvalidatedDate",
                table: "ReportFeedbacks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvalidationReason",
                table: "ReportFeedbacks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedbacks_InvalidatedByUserId",
                table: "ReportFeedbacks",
                column: "InvalidatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportFeedbacks_Users_InvalidatedByUserId",
                table: "ReportFeedbacks",
                column: "InvalidatedByUserId",
                principalTable: "Users",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportFeedbacks_Users_InvalidatedByUserId",
                table: "ReportFeedbacks");

            migrationBuilder.DropIndex(
                name: "IX_ReportFeedbacks_InvalidatedByUserId",
                table: "ReportFeedbacks");

            migrationBuilder.DropColumn(
                name: "InvalidatedByUserId",
                table: "ReportFeedbacks");

            migrationBuilder.DropColumn(
                name: "InvalidatedDate",
                table: "ReportFeedbacks");

            migrationBuilder.DropColumn(
                name: "InvalidationReason",
                table: "ReportFeedbacks");
        }
    }
}
