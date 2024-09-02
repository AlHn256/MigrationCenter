// <copyright file="20190930234942_UserAddListsOfMigrant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserAddListsOfMigrant : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonInfoKey",
                table: "ListOfMigrantes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ListOfMigrantes_PersonInfoKey",
                table: "ListOfMigrantes",
                column: "PersonInfoKey");

            migrationBuilder.AddForeignKey(
                name: "FK_ListOfMigrantes_Users_PersonInfoKey",
                table: "ListOfMigrantes",
                column: "PersonInfoKey",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListOfMigrantes_Users_PersonInfoKey",
                table: "ListOfMigrantes");

            migrationBuilder.DropIndex(
                name: "IX_ListOfMigrantes_PersonInfoKey",
                table: "ListOfMigrantes");

            migrationBuilder.DropColumn(
                name: "PersonInfoKey",
                table: "ListOfMigrantes");
        }
    }
}
