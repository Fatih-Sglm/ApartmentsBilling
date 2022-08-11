using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentsBilling.DataAccesLayer.Migrations
{
    public partial class UpdateFlat1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRented",
                table: "Flats",
                newName: "IsRented");

            migrationBuilder.RenameColumn(
                name: "isEmpty",
                table: "Flats",
                newName: "IsEmpty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRented",
                table: "Flats",
                newName: "isRented");

            migrationBuilder.RenameColumn(
                name: "IsEmpty",
                table: "Flats",
                newName: "isEmpty");
        }
    }
}
