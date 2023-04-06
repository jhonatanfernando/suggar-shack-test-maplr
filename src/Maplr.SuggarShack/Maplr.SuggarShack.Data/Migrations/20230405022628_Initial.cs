using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maplr.SuggarShack.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "Stock", "Type" },
                values: new object[] { new Guid("070356bc-fa7b-49b5-9efa-37c6be8376b7"), "Fine nose that opens to the scents of maple and a quite supported final. In mouth, it has a pure taste and an ample texture, unveiling fully the maple aromas.", "https://www.camaplepremium.com/wp-content/uploads/2017/11/Sirop_amber_250ml-800x533.jpg", "AMBER MAPLE SYRUP", 12.949999999999999, 100, 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "Stock", "Type" },
                values: new object[] { new Guid("a3f94f7c-2f43-4299-a02f-26f04f8e0929"), "Strong nose that opens on a caramel and butter perfume. In mouth, a franc maple flavor and a creamy texture.", "https://www.camaplepremium.com/wp-content/uploads/2018/05/Sirop_dark_100ml.jpg", "DARK MAPLE SYRUP", 7.9500000000000002, 100, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "Stock", "Type" },
                values: new object[] { new Guid("b241f0d7-a7e9-41dd-8f0a-bab78056e673"), "Soft nose that opens on a caramel and butter perfume. In mouth, a franc maple flavor and a creamy texture.", "https://www.finemapleproducts.com/wp-content/uploads/2018/06/803_2461.jpg", "CLEAR MAPLE SYRUP", 6.9500000000000002, 100, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
