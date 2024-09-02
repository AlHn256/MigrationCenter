using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiDemoMC.Migrations
{
    public partial class AddPayList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayerId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    PurposeOfPayment = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsDebet = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayList", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayList");
        }
    }
}
