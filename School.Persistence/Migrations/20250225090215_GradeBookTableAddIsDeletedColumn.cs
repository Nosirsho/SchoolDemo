using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GradeBookTableAddIsDeletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DaletedAt",
                table: "GradeBooks",
                type: "timestamp with time zone",
                nullable: true
                );

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GradeBooks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaletedAt",
                table: "GradeBooks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GradeBooks");
        }
    }
}
