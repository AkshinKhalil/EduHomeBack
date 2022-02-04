using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEnd.Migrations
{
    public partial class addProfessionColumnToSpeakerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Speakers",
                maxLength: 70,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Speakers");
        }
    }
}
