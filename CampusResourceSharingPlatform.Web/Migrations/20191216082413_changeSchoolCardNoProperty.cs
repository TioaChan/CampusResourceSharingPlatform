using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusResourceSharingPlatform.Web.Migrations
{
    public partial class changeSchoolCardNoProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SchoolCardNo",
                table: "AspNetUsers",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 12);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SchoolCardNo",
                table: "AspNetUsers",
                type: "int",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
