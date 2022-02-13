using Microsoft.EntityFrameworkCore.Migrations;

namespace Grad_Proj.Migrations
{
    public partial class cover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortfolioLink",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "PortfolioLink",
                table: "Authors");
        }
    }
}
