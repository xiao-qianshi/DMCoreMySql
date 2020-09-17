using Microsoft.EntityFrameworkCore.Migrations;

namespace Dmt.DM.EntityFrameWork.Migrations
{
    public partial class m_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "F_SerialNumber",
                table: "Sys_UserEntities",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "F_SerialNumber",
                table: "Sys_UserEntities",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
