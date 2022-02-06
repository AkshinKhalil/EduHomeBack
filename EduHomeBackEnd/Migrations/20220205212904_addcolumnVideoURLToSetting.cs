using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEnd.Migrations
{
    public partial class addcolumnVideoURLToSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Settings",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Settings");
        }
    }
}
