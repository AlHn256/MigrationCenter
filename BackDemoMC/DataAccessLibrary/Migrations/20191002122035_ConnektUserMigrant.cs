// <copyright file="20191002122035_ConnektUserMigrant.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ConnektUserMigrant : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMigrant_Users_UserId",
                table: "UserMigrant");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMigrant_Users_MigrantId",
                table: "UserMigrant",
                column: "MigrantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMigrant_Users_MigrantId",
                table: "UserMigrant");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMigrant_Users_UserId",
                table: "UserMigrant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
