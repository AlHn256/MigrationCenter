using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDemoMC.Migrations
{
    public partial class AddBuhalter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateLicens",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicensNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qualification",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDateLicens",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateLicens",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LicensNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StartDateLicens",
                table: "Users");
        }
    }
}
