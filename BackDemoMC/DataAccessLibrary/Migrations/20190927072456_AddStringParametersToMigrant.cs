// <copyright file="20190927072456_AddStringParametersToMigrant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddStringParametersToMigrant : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassNumber",
                table: "Migrant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassSerri",
                table: "Migrant",
                nullable: true);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassNumber",
                table: "Migrant");

            migrationBuilder.DropColumn(
                name: "PassSerri",
                table: "Migrant");
        }
    }
}
