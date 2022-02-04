using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEnd.Migrations
{
    public partial class addCourseFeaturesCourseID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeaturesId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseFeaturesId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFeaturesId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseFeatures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseFeatures_Courses_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseFeatures");

            migrationBuilder.AddColumn<int>(
                name: "CourseFeaturesId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseFeaturesId",
                table: "Courses",
                column: "CourseFeaturesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseFeatures_CourseFeaturesId",
                table: "Courses",
                column: "CourseFeaturesId",
                principalTable: "CourseFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
