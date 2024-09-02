// <copyright file="20191001085332_ServiceDataInit.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ServiceDataInit : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Cost", "IsDelete", "Name" },
                values: new object[] { 1, 400, false, "Оплата НДФЛ" });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Cost", "IsDelete", "Name" },
                values: new object[] { 2, 600, false, "Мед. страхование" });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Cost", "IsDelete", "Name" },
                values: new object[] { 3, 1100, false, "Оформление ВНЖ" });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
