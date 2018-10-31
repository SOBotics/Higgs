using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Higgs.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Scopes",
                table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Scopes", x => x.Name); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.AccountId); });

            migrationBuilder.CreateTable(
                "UserScopes",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ScopeName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScopes", x => x.Id);
                    table.ForeignKey(
                        "FK_UserScopes_Scopes_ScopeName",
                        x => x.ScopeName,
                        "Scopes",
                        "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_UserScopes_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_UserScopes_ScopeName",
                "UserScopes",
                "ScopeName");

            migrationBuilder.CreateIndex(
                "IX_UserScopes_UserId",
                "UserScopes",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "UserScopes");

            migrationBuilder.DropTable(
                "Scopes");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}