using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Higgs.Server.Migrations
{
    public partial class WipeUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
delete from ""public"".""Users""
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
