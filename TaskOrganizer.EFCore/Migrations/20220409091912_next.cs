using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskOrganizer.EFCore.Migrations
{
    public partial class next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "PomodoroModels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "PomodoroModels");
        }
    }
}
