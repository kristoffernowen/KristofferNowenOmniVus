using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewOmniVus.Migrations.SecondDb
{
    public partial class scuserWithEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SecondUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SecondUsers");
        }
    }
}
