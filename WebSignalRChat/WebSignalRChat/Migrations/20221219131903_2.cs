using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSignalRChat.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "SendModels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeOnly",
                table: "SendModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "SendModels");

            migrationBuilder.DropColumn(
                name: "TimeOnly",
                table: "SendModels");
        }
    }
}
