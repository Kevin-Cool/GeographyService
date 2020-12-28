using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country_ID",
                table: "Rivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Country_ID",
                table: "Rivers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
