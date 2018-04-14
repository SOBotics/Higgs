using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class AddFunctionalityToDisableFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredActions",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActionable",
                table: "Feedbacks",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Feedbacks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActionable",
                table: "Feedbacks",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "RequiredActions",
                table: "Feedbacks",
                nullable: true);
        }
    }
}
