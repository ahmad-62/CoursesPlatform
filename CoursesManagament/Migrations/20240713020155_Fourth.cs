using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursesManagament.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Trainer",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_courses_Course",
                table: "Trainee_courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_courses_Trainee",
                table: "Trainee_courses");

           

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Course",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Trainer",
                table: "Course",
                column: "Trainer_Id",
                principalTable: "Trainer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_courses_Course",
                table: "Trainee_courses",
                column: "Course_Id",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_courses_Trainee",
                table: "Trainee_courses",
                column: "Trainee_Id",
                principalTable: "Trainee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Course_CourseId",
                table: "Units");
            migrationBuilder.AddForeignKey(
                name: "FK_Units_Course_CourseId",
                table: "Units",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
              onDelete: ReferentialAction.Cascade

                );
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Units",
                table: "Lessons"
                );
            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Units",
                table: "Lessons",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
              onDelete: ReferentialAction.Cascade
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Trainer",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_courses_Course",
                table: "Trainee_courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainee_courses_Trainee",
                table: "Trainee_courses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Lessons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Trainer",
                table: "Course",
                column: "Trainer_Id",
                principalTable: "Trainer",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_courses_Course",
                table: "Trainee_courses",
                column: "Course_Id",
                principalTable: "Course",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainee_courses_Trainee",
                table: "Trainee_courses",
                column: "Trainee_Id",
                principalTable: "Trainee",
                principalColumn: "ID");
        }
    }
}
