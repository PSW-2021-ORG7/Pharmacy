using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class MedicineMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicine_Name",
                table: "Medicine");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_MedicineId",
                table: "Medicine",
                column: "MedicineId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicine_MedicineId",
                table: "Medicine");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Name",
                table: "Medicine",
                column: "Name",
                unique: true);
        }
    }
}
