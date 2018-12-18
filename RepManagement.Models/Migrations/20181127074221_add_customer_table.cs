using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepManagement.Models.Migrations
{
    public partial class add_customer_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SerialNum = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    TypeID = table.Column<Guid>(nullable: false),
                    Style = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Demand = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    CustomerEval = table.Column<string>(nullable: true),
                    FactoryEval = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    ManagerPhone = table.Column<string>(nullable: true),
                    ManagerName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Years = table.Column<double>(nullable: true),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
