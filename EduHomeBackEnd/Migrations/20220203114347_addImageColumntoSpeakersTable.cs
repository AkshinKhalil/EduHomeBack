using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEnd.Migrations
{
    public partial class addImageColumntoSpeakersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Speakers",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Speakers");
        }
    }
}
