using Microsoft.EntityFrameworkCore.Migrations;

namespace Order2Go.Migrations
{
    public partial class AddDateDacturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mes",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "year",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "year",
                table: "Facturas");
        }
    }
}
