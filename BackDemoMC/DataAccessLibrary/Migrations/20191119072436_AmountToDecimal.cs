using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDemoMC.Migrations
{
    public partial class AmountToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "PayList",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "PayList",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
