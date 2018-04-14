using Microsoft.EntityFrameworkCore.Migrations;

namespace Higgs.Server.Migrations
{
    public partial class RemoveExistingFeedbackTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
delete from ""public"".""Feedbacks""
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
