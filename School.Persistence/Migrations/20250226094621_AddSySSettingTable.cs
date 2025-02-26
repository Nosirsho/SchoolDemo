using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSySSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysSettingType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysSettingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysSetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    SysSettingTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IntegerValue = table.Column<int>(type: "integer", nullable: false),
                    DateTmeValue = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: false),
                    StringValue = table.Column<string>(type: "text", nullable: false),
                    GuidValue = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysSetting_SysSettingType_SysSettingTypeId",
                        column: x => x.SysSettingTypeId,
                        principalTable: "SysSettingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SysSettingType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("09166a2f-2541-4787-b6ba-0bd45941127d"), "Integer" },
                    { new Guid("6be94388-5f91-4634-b749-70259437bab3"), "DateTime" },
                    { new Guid("b7bc1d4e-c906-4f32-83d9-76c016f82de6"), "Boolean" },
                    { new Guid("bcd76746-9bf8-4012-93bc-a4d1392c60cf"), "Guid" },
                    { new Guid("c6bc2289-451d-408b-b97f-35fa42974c05"), "String" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysSetting_SysSettingTypeId",
                table: "SysSetting",
                column: "SysSettingTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysSetting");

            migrationBuilder.DropTable(
                name: "SysSettingType");
        }
    }
}
