using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachineSimulator.Data.Migrations
{
    public partial class AddIsEspressorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEspressor",
                table: "EspressoMachines",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEspressor",
                table: "EspressoMachines");
        }
    }
}
