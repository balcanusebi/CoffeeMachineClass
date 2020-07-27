using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachineSimulator.Data.Migrations
{
    public partial class AddEspressoMachineForeignKeyToCoffee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EspressoMachineId",
                table: "Coffees",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_EspressoMachineId",
                table: "Coffees",
                column: "EspressoMachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_EspressoMachines_EspressoMachineId",
                table: "Coffees",
                column: "EspressoMachineId",
                principalTable: "EspressoMachines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_EspressoMachines_EspressoMachineId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_EspressoMachineId",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "EspressoMachineId",
                table: "Coffees");
        }
    }
}
