using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRate.Data.Migrations
{
    public partial class NewCyrrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "SaleNode",
                table: "Sites",
                newName: "UsdSellNode");

            migrationBuilder.RenameColumn(
                name: "BuyNode",
                table: "Sites",
                newName: "UsdBuyNode");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "Currencies",
                newName: "UsdSell");

            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                table: "Currencies",
                newName: "UsdBuy");

            migrationBuilder.AddColumn<string>(
                name: "EurBuyNode",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EurSellNode",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RubBuyNode",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RubSellNode",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "EurBuy",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EurSell",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RubBuy",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RubSell",
                table: "Currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EurBuyNode",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "EurSellNode",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "RubBuyNode",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "RubSellNode",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "EurBuy",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "EurSell",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "RubBuy",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "RubSell",
                table: "Currencies");

            migrationBuilder.RenameColumn(
                name: "UsdSellNode",
                table: "Sites",
                newName: "SaleNode");

            migrationBuilder.RenameColumn(
                name: "UsdBuyNode",
                table: "Sites",
                newName: "BuyNode");

            migrationBuilder.RenameColumn(
                name: "UsdSell",
                table: "Currencies",
                newName: "SalePrice");

            migrationBuilder.RenameColumn(
                name: "UsdBuy",
                table: "Currencies",
                newName: "BuyPrice");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Currencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
