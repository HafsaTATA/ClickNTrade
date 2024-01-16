using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopEnLigne.Migrations
{
    /// <inheritdoc />
    public partial class updateUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Categorie_CategorieId",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Bien_User_UserId",
                table: "Bien");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Bien",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Categorie_CategorieId",
                table: "Bien",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_User_UserId",
                table: "Bien",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bien_Categorie_CategorieId",
                table: "Bien");

            migrationBuilder.DropForeignKey(
                name: "FK_Bien_User_UserId",
                table: "Bien");

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Bien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Bien",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_Categorie_CategorieId",
                table: "Bien",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bien_User_UserId",
                table: "Bien",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
