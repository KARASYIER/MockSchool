using Microsoft.EntityFrameworkCore.Migrations;

namespace MockSchool.Web.Migrations
{
    public partial class AddPhotoPathToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhohtPath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "Major", "Name", "PhohtPath" },
                values: new object[] { 2, "karasyier@hotmail.com", 3, "Karasyier", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "PhohtPath",
                table: "Students");
        }
    }
}
