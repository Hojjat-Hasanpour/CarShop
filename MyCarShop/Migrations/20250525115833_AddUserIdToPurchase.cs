using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCarShop.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_ApplicationUserId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ApplicationUserId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Purchases");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Purchases");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ApplicationUserId",
                table: "Purchases",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_ApplicationUserId",
                table: "Purchases",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
