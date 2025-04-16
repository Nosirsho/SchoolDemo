using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToGradeLevelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DaletedAt",
                table: "GradeLevels",
                type: "timestamp with time zone",
                nullable: true
            );

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GradeLevels",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaletedAt",
                table: "GradeLevels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GradeLevels");
        }
    }
}
