using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMachineSimulator.Data.Migrations
{
    public partial class CreateCoffeeDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeDataEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    SensorType = table.Column<string>(nullable: true),
                    SensorValue = table.Column<int>(nullable: false),
                    RecordingTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeDataEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeDataEntities");
        }
    }
}
