using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class FeedbackAndReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Homepage",
                table: "Bots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Bots",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BotId = table.Column<int>(nullable: false),
                    Colour = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BotId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reasons_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AuthorName = table.Column<string>(nullable: true),
                    AuthorReputation = table.Column<int>(nullable: true),
                    BotId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ContentCreationDate = table.Column<DateTime>(nullable: true),
                    ContentUrl = table.Column<string>(nullable: true),
                    DetectedDate = table.Column<DateTime>(nullable: true),
                    DetectionScore = table.Column<double>(nullable: true),
                    ObfuscatedContent = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportAllowedFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FeedbackId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAllowedFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportAllowedFeedbacks_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportAllowedFeedbacks_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Confidence = table.Column<double>(nullable: true),
                    ReasonId = table.Column<int>(nullable: false),
                    ReportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportReasons_Reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "Reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportReasons_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BotId",
                table: "Feedbacks",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_BotId",
                table: "Reasons",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAllowedFeedbacks_FeedbackId",
                table: "ReportAllowedFeedbacks",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportAllowedFeedbacks_ReportId",
                table: "ReportAllowedFeedbacks",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReasons_ReasonId",
                table: "ReportReasons",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReasons_ReportId",
                table: "ReportReasons",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_BotId",
                table: "Reports",
                column: "BotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportAllowedFeedbacks");

            migrationBuilder.DropTable(
                name: "ReportReasons");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropColumn(
                name: "Homepage",
                table: "Bots");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Bots");
        }
    }
}
