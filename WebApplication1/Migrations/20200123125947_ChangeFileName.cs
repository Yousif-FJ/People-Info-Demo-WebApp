﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class ChangeFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}