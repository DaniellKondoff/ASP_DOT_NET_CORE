using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace _1.StudentSystem.Migrations
{
    public partial class NewTableLicenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_License_Resources_ResourceId",
                table: "License");

            migrationBuilder.DropPrimaryKey(
                name: "PK_License",
                table: "License");

            migrationBuilder.RenameTable(
                name: "License",
                newName: "Liceses");

            migrationBuilder.RenameIndex(
                name: "IX_License_ResourceId",
                table: "Liceses",
                newName: "IX_Liceses_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Liceses",
                table: "Liceses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Liceses_Resources_ResourceId",
                table: "Liceses",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liceses_Resources_ResourceId",
                table: "Liceses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Liceses",
                table: "Liceses");

            migrationBuilder.RenameTable(
                name: "Liceses",
                newName: "License");

            migrationBuilder.RenameIndex(
                name: "IX_Liceses_ResourceId",
                table: "License",
                newName: "IX_License_ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_License",
                table: "License",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_License_Resources_ResourceId",
                table: "License",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
