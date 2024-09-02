// <copyright file="20190924130509_DataInit.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DataInit : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "jurperson" },
                    { 3, "agent" },
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bank_Kor_Sch", "Bank_Rasch_Sch", "Bik_Bank", "Birthdate", "FIO", "GradationFIO", "INN", "IsDelete", "KPP", "Kor_Sch", "Mail", "Main_Document", "Name", "Pass", "Phone", "Rasch_Sch", "RoleId", "Token", "Ur_Adress" },
                values: new object[] { 1, string.Empty, string.Empty, string.Empty, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), string.Empty, string.Empty, 0, false, 0, 0, string.Empty, string.Empty, "admin", string.Empty, string.Empty, 0, 1, string.Empty, string.Empty });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
