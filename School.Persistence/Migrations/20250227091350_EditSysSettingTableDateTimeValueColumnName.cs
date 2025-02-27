using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditSysSettingTableDateTimeValueColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTmeValue",
                table: "SysSetting",
                newName: "DateTimeValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeValue",
                table: "SysSetting",
                newName: "DateTmeValue");
        }
    }
}
