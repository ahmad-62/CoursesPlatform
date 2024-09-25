using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursesManagament.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                table: "AspNetUsers",
                name: "FK_AspNetUsers_Trainer_TrainerId"
                );
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trainer_TrainerId",
                table: "AspNetUsers",
                column: "TrainerId",
                principalTable: "Trainer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
