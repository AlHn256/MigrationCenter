using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDemoMC.Migrations
{
    public partial class IsPaidDelitedFromOreder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Order");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Order",
                nullable: false,
                defaultValue: false);
        }
    }
}
