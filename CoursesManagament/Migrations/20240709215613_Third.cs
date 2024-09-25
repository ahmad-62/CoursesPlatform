using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursesManagament.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename column from Course_id to UnitId
            migrationBuilder.RenameColumn(
                name: "Course_id",
                table: "Lessons",
                newName: "UnitId");

            // Rename the foreign key constraint from FK_Lessons_Course to FK_Lessons_Unit
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Course",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Units",
                table: "Lessons",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            // Alter column Type to have a max length of 200
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Lessons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert column name back to Course_id
            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Lessons",
                newName: "Course_id");

            // Revert the foreign key constraint name back to FK_Lessons_Course
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Units",
                table: "Lessons");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Course",
                table: "Lessons",
                column: "Course_id",
                principalTable: "Units",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            // Revert column Type back to its original state
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
