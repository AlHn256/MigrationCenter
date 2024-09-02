// <copyright file="20190927072147_DelIntParametersFromMigrant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DelIntParametersFromMigrant : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassNumber",
                table: "Migrant");

            migrationBuilder.DropColumn(
                name: "PassSerri",
                table: "Migrant");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassNumber",
                table: "Migrant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PassSerri",
                table: "Migrant",
                nullable: false,
                defaultValue: 0);
        }
    }
}
