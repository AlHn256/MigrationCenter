using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDemoMC.Migrations
{
    public partial class StatusChangeTypeInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
