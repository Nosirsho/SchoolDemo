using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeLeveltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GradeLevelId",
                table: "Teachers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GradeLevelId",
                table: "Students",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EntryYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_GradeLevelId",
                table: "Teachers",
                column: "GradeLevelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeLevelId",
                table: "Students",
                column: "GradeLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_GradeLevels_GradeLevelId",
                table: "Students",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_GradeLevels_GradeLevelId",
                table: "Teachers",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_GradeLevels_GradeLevelId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_GradeLevels_GradeLevelId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "GradeLevels");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_GradeLevelId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_GradeLevelId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GradeLevelId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "GradeLevelId",
                table: "Students");
        }
    }
}
