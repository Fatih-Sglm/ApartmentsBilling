using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentsBilling.DataAccesLayer.Migrations
{
    public partial class AddMessageAndAndSomeUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userPasswords_Users_UserId",
                table: "userPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userPasswords",
                table: "userPasswords");

            migrationBuilder.RenameTable(
                name: "userPasswords",
                newName: "UserPasswords");

            migrationBuilder.RenameIndex(
                name: "IX_userPasswords_UserId",
                table: "UserPasswords",
                newName: "IX_UserPasswords_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPasswords",
                table: "UserPasswords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPasswords_Users_UserId",
                table: "UserPasswords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPasswords_Users_UserId",
                table: "UserPasswords");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPasswords",
                table: "UserPasswords");

            migrationBuilder.RenameTable(
                name: "UserPasswords",
                newName: "userPasswords");

            migrationBuilder.RenameIndex(
                name: "IX_UserPasswords_UserId",
                table: "userPasswords",
                newName: "IX_userPasswords_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userPasswords",
                table: "userPasswords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userPasswords_Users_UserId",
                table: "userPasswords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
