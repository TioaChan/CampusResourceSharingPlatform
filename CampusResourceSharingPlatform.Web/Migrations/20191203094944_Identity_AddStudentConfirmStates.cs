using Microsoft.EntityFrameworkCore.Migrations;

namespace CampusResourceSharingPlatform.Web.Migrations
{
    public partial class Identity_AddStudentConfirmStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StudentIdentityConfirmed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentIdentityConfirmed",
                table: "AspNetUsers");
        }
    }
}
