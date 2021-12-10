using Microsoft.EntityFrameworkCore.Migrations;

namespace Order2Go.Migrations
{
    public partial class AddTelefonoToComercio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Comercios",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Comercios");
        }
    }
}
