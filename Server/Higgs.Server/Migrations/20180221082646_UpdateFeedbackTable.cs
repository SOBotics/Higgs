using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class UpdateFeedbackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActionable",
                table: "Feedbacks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequiredActions",
                table: "Feedbacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "IsActionable",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "RequiredActions",
                table: "Feedbacks");
        }
    }
}
