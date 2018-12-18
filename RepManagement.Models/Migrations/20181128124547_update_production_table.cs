using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepManagement.Models.Migrations
{
    public partial class update_production_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StyleTypeId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StyleTypeId",
                table: "Productions");
        }
    }
}
