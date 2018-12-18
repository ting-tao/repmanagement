using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepManagement.Models.Migrations
{
    public partial class update_customer_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNum",
                table: "Productions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "DecorateCost",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DecorateVendorId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "MainCost",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MainVendorId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MatTypeId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MouldId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "PacketCost",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProcessCost",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SweatbandCost",
                table: "Productions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SweatbandVendorId",
                table: "Productions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "TransportCost",
                table: "Productions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DecorateCost",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "DecorateVendorId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "MainCost",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "MainVendorId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "MatTypeId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "MouldId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "PacketCost",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "ProcessCost",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "SweatbandCost",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "SweatbandVendorId",
                table: "Productions");

            migrationBuilder.DropColumn(
                name: "TransportCost",
                table: "Productions");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNum",
                table: "Productions",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
