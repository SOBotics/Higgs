using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class TidyUpReportRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ObfuscatedContent",
                table: "Reports");

            migrationBuilder.CreateTable(
                name: "ContentFragments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Content = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false),
                    RequiredScope = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFragments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFragments_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ReportId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportAttributes_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentFragments_ReportId",
                table: "ContentFragments",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAttributes_ReportId",
                table: "ReportAttributes",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentFragments");

            migrationBuilder.DropTable(
                name: "ReportAttributes");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObfuscatedContent",
                table: "Reports",
                nullable: true);
        }
    }
}
