using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepManagement.Models.Migrations
{
    public partial class updateimgtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RelatedItemId",
                table: "SystemImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedItemId",
                table: "SystemImages");
        }
    }
}
