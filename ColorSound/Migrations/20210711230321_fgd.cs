using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Grad_Proj.Migrations
{
    public partial class fgd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Authors_AuthorId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Since",
                table: "AuthorToAuthor",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemLikes_AuthorId",
                table: "ItemLikes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemLikes_Authors_AuthorId",
                table: "ItemLikes",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Authors_AuthorId",
                table: "Items",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemLikes_Authors_AuthorId",
                table: "ItemLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Authors_AuthorId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropIndex(
                name: "IX_ItemLikes_AuthorId",
                table: "ItemLikes");

            migrationBuilder.DropColumn(
                name: "Since",
                table: "AuthorToAuthor");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Authors_AuthorId",
                table: "Items",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
