using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreIS.Data.Migrations
{
    public partial class AddNameAndCreatedAtDatePropertiesInSoppingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ShoppingLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingLists");
        }
    }
}
