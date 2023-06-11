using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenerGrain.Data.Migrations
{
    public partial class AdiçãodeordememModulos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Module",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Module");
        }
    }
}
