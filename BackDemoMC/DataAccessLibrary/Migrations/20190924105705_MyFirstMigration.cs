// <copyright file="20190924105705_MyFirstMigration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListOfMigrantes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListOfMigrantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Migrant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    PassNumber = table.Column<int>(nullable: false),
                    PassSerri = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Migrant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MigrationCenteres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Adress = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MigrationCenteres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    INN = table.Column<int>(nullable: false),
                    KPP = table.Column<int>(nullable: false),
                    Rasch_Sch = table.Column<int>(nullable: false),
                    Bank_Rasch_Sch = table.Column<string>(nullable: true),
                    Kor_Sch = table.Column<int>(nullable: false),
                    Bank_Kor_Sch = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Ur_Adress = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    FIO = table.Column<string>(nullable: true),
                    GradationFIO = table.Column<string>(nullable: true),
                    Main_Document = table.Column<string>(nullable: true),
                    Bik_Bank = table.Column<string>(nullable: true),
                    Pass = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMigrant",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MigrantId = table.Column<int>(nullable: false),
                    Order = table.Column<byte>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMigrant", x => new { x.UserId, x.MigrantId });
                    table.ForeignKey(
                        name: "FK_UserMigrant_Migrant_MigrantId",
                        column: x => x.MigrantId,
                        principalTable: "Migrant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMigrant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMigrant_MigrantId",
                table: "UserMigrant",
                column: "MigrantId");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListOfMigrantes");

            migrationBuilder.DropTable(
                name: "MigrationCenteres");

            migrationBuilder.DropTable(
                name: "UserMigrant");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Migrant");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
