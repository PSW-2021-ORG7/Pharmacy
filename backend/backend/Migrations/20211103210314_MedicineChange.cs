using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class MedicineChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Medicine",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "medicine_id",
                table: "Medicine",
                newName: "MedicineId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Medicine",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DosageInMilligrams",
                table: "Medicine",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<List<string>>(
                name: "SideEffect",
                table: "Medicine",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WayOfConsumption",
                table: "Medicine",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Allergen",
                columns: table => new
                {
                    AllergenId = table.Column<Guid>(type: "uuid", nullable: false),
                    Other = table.Column<string>(type: "text", nullable: true),
                    MedicineNames = table.Column<List<string>>(type: "text[]", nullable: true),
                    IngredientNames = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergen", x => x.AllergenId);
                    table.ForeignKey(
                        name: "FK_Allergen_Medicine_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Medicine",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergen");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "DosageInMilligrams",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "SideEffect",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "WayOfConsumption",
                table: "Medicine");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Medicine",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "Medicine",
                newName: "medicine_id");
        }
    }
}
