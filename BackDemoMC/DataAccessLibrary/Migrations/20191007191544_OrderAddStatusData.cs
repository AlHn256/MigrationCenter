// <copyright file="20191007191544_OrderAddStatusData.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class OrderAddStatusData : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Cost", "IsDelete", "Name" },
                values: new object[,]
                {
                    { 4, 1000, false, "Мед.Освидетельствование" },
                    { 5, 800, false, "Экзамен русского языка" },
                    { 6, 600, false, "Базовый экзамен русского языка" },
                    { 7, 600, false, "Заключение труд. договора" },
                });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");
        }
    }
}
