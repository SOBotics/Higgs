using Microsoft.EntityFrameworkCore.Migrations;

namespace Higgs.Server.Migrations
{
    public partial class AddReviewAndConflictedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Conflicted",
                table: "Reports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresReview",
                table: "Reports",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conflicted",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "RequiresReview",
                table: "Reports");
        }
    }
}
