// <copyright file="20191030134228_OrderItemsFkFix.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class OrderItemsFkFix : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Service_serviceId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ServicetId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "serviceId",
                table: "OrderItem",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_serviceId",
                table: "OrderItem",
                newName: "IX_OrderItem_ServiceId");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Service_ServiceId",
                table: "OrderItem",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Service_ServiceId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "OrderItem",
                newName: "serviceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ServiceId",
                table: "OrderItem",
                newName: "IX_OrderItem_serviceId");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "serviceId",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ServicetId",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Service_serviceId",
                table: "OrderItem",
                column: "serviceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
