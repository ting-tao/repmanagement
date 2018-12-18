using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepManagement.Models.Migrations
{
    public partial class updateimgtable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedItemId",
                table: "SystemImages");

            migrationBuilder.CreateTable(
                name: "ProductionImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    ProdId = table.Column<Guid>(nullable: false),
                    IsCover = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionImages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionImages");

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedItemId",
                table: "SystemImages",
                nullable: true);
        }
    }
}
