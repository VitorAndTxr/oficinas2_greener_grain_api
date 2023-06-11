using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenerGrain.Data.Migrations
{
    public partial class AdiçãodoMACasunidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MAC",
                table: "Unit",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAC",
                table: "Unit");
        }
    }
}
