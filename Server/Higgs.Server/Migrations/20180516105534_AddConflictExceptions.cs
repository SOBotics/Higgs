using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Higgs.Server.Migrations
{
    public partial class AddConflictExceptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConflictExceptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BotId = table.Column<int>(nullable: true),
                    IsConflict = table.Column<bool>(nullable: false),
                    ReportId = table.Column<int>(nullable: true),
                    RequiresAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConflictExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConflictExceptions_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConflictExceptions_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConflictExceptionFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ConflictExceptionId = table.Column<int>(nullable: false),
                    FeedbackId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConflictExceptionFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConflictExceptionFeedbacks_ConflictExceptions_ConflictExceptionId",
                        column: x => x.ConflictExceptionId,
                        principalTable: "ConflictExceptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConflictExceptionFeedbacks_Feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConflictExceptionFeedbacks_ConflictExceptionId",
                table: "ConflictExceptionFeedbacks",
                column: "ConflictExceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConflictExceptionFeedbacks_FeedbackId",
                table: "ConflictExceptionFeedbacks",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_ConflictExceptions_BotId",
                table: "ConflictExceptions",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_ConflictExceptions_ReportId",
                table: "ConflictExceptions",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConflictExceptionFeedbacks");

            migrationBuilder.DropTable(
                name: "ConflictExceptions");
        }
    }
}
