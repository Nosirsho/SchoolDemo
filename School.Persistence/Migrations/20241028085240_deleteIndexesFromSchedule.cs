using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteIndexesFromSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_GradeLevelId",
                table: "Schedules");
            migrationBuilder.DropIndex(
                name: "IX_Schedules_LessonId",
                table: "Schedules");
            migrationBuilder.DropIndex(
                name: "IX_Schedules_TeacherId",
                table: "Schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex( 
                name: "IX_Schedules_LessonId",
                table: "Schedules",
                column: "LessonId",
                unique: true);
            migrationBuilder.CreateIndex( 
                name: "IX_Schedules_TeacherId",
                table: "Schedules",
                column: "TeacherId",
                unique: true);
        }
    }
}
