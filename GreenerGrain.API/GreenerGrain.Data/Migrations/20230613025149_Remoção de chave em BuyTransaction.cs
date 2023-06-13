using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenerGrain.Data.Migrations
{
    public partial class RemoçãodechaveemBuyTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyTransaction",
                table: "BuyTransaction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyTransaction",
                table: "BuyTransaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BuyTransaction_BuyerId",
                table: "BuyTransaction",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BuyTransaction",
                table: "BuyTransaction");

            migrationBuilder.DropIndex(
                name: "IX_BuyTransaction_BuyerId",
                table: "BuyTransaction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuyTransaction",
                table: "BuyTransaction",
                columns: new[] { "BuyerId", "ModuleId", "GrainId" });
        }
    }
}
