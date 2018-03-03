using Microsoft.EntityFrameworkCore.Migrations;

namespace Higgs.Server.Migrations
{
    public partial class AddTrippedFieldToReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Tripped",
                table: "ReportReasons",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tripped",
                table: "ReportReasons");
        }
    }
}
