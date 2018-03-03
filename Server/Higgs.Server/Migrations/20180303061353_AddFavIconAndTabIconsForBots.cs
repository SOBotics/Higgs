using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class AddFavIconAndTabIconsForBots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavIcon",
                table: "Bots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TabTitle",
                table: "Bots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavIcon",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "TabTitle",
                table: "Bots");
        }
    }
}
