using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPSBookTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBukuHistory : Migration
    {
        /// <inheritdoc />


        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BukuHistory_Buku",
                table: "BukuHistory");

            migrationBuilder.AlterColumn<int>(
                name: "BukuId",
                table: "BukuHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BukuHistory_Buku",
                table: "BukuHistory",
                column: "BukuId",
                principalTable: "Buku",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }



        /// <inheritdoc />


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BukuHistory_Buku",
                table: "BukuHistory");

            migrationBuilder.AlterColumn<int>(
                name: "BukuId",
                table: "BukuHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BukuHistory_Buku",
                table: "BukuHistory",
                column: "BukuId",
                principalTable: "Buku",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }


    }
}
