using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentsBilling.DataAccesLayer.Migrations
{
    public partial class IsPaymentUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isPayment",
                table: "Bills",
                newName: "IsPayment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPayment",
                table: "Bills",
                newName: "isPayment");
        }
    }
}
