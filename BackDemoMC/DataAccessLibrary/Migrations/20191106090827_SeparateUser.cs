// <copyright file="20191106090827_SeparateUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SeparateUser : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrAgent_Phone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<int>(
                name: "UrAgentId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UrAgentId",
                table: "Order",
                column: "UrAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_UrAgentId",
                table: "Order",
                column: "UrAgentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_UrAgentId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_UrAgentId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UrAgent_Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UrAgentId",
                table: "Order");
        }
    }
}
