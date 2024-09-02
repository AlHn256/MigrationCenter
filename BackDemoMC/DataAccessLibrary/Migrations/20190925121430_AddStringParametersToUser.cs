// <copyright file="20190925121430_AddStringParametersToUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddStringParametersToUser : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "INN",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KPP",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kor_Sch",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rasch_Sch",
                table: "Users",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "INN",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "KPP",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Kor_Sch",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Rasch_Sch",
                table: "Users");
        }
    }
}
