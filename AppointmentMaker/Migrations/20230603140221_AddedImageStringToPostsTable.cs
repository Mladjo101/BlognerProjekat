using Microsoft.EntityFrameworkCore.Migrations;

namespace AppointmentMaker.Migrations
{
    public partial class AddedImageStringToPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "postImageString",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "postImageString",
                table: "Posts");
        }
    }
}
