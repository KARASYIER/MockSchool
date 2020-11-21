using Microsoft.EntityFrameworkCore.Migrations;

namespace MockSchool.Web.Migrations
{
    public partial class EditPhotoPathName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhohtPath",
                table: "Students",
                newName: "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Students",
                newName: "PhohtPath");
        }
    }
}
