using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AllergenChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergen_Medicine_AllergenId",
                table: "Allergen");

            migrationBuilder.AddColumn<Guid>(
                name: "MedicineId",
                table: "Allergen",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergen_MedicineId",
                table: "Allergen",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergen_Medicine_MedicineId",
                table: "Allergen",
                column: "MedicineId",
                principalTable: "Medicine",
                principalColumn: "MedicineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergen_Medicine_MedicineId",
                table: "Allergen");

            migrationBuilder.DropIndex(
                name: "IX_Allergen_MedicineId",
                table: "Allergen");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Allergen");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergen_Medicine_AllergenId",
                table: "Allergen",
                column: "AllergenId",
                principalTable: "Medicine",
                principalColumn: "MedicineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
