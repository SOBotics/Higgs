using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class AddContentIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentSite",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Reports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ContentSite",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Reports");
        }
    }
}
