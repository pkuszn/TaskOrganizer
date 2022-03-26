using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskOrganizer.EFCore.Migrations
{
    public partial class extensionoftaskmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DoneTaskDate",
                table: "TaskModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "TaskModels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TaskModels");

            migrationBuilder.DropColumn(
                name: "DoneTaskDate",
                table: "TaskModels");

            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "TaskModels");
        }
    }
}
