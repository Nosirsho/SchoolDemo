using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSysSettingTableColumnToNotNullFalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeValue",
                table: "SysSetting",
                nullable: true);
            migrationBuilder.AlterColumn<int>(
                name: "IntegerValue",
                table: "SysSetting",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "BooleanValue",
                table: "SysSetting",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "StringValue",
                table: "SysSetting",
                nullable: true);
            migrationBuilder.AlterColumn<Guid>(
                name: "GuidValue",
                table: "SysSetting",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeValue",
                table: "SysSetting",
                nullable: false);
            migrationBuilder.AlterColumn<int>(
                name: "IntegerValue",
                table: "SysSetting",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "BooleanValue",
                table: "SysSetting",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "StringValue",
                table: "SysSetting",
                nullable: false);
            migrationBuilder.AlterColumn<Guid>(
                name: "GuidValue",
                table: "SysSetting",
                nullable: false);
        }
    }
}
