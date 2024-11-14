using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveClmnToScheduleTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Schedules",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Schedules",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Schedules");
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Schedules");
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Schedules");
        }
    }
}
