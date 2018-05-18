using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class TrackRequiredFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredFeedback",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredFeedbackConflicted",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredFeedback",
                table: "ConflictExceptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredFeedback",
                table: "Bots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredFeedbackConflicted",
                table: "Bots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
UPDATE ""Reports""
SET ""RequiredFeedback"" = 2
");

            migrationBuilder.Sql(@"
UPDATE ""Reports""
SET ""RequiredFeedbackConflicted"" = 4
");

            migrationBuilder.Sql(@"
UPDATE ""Bots""
SET ""RequiredFeedback"" = 2
");

            migrationBuilder.Sql(@"
UPDATE ""Bots""
SET ""RequiredFeedbackConflicted"" = 4
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredFeedback",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RequiredFeedbackConflicted",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RequiredFeedback",
                table: "ConflictExceptions");

            migrationBuilder.DropColumn(
                name: "RequiredFeedback",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "RequiredFeedbackConflicted",
                table: "Bots");
        }
    }
}
