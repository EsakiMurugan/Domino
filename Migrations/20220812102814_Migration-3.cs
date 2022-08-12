using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domino.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartTypeID",
                table: "customers",
                type: "int",
                nullable: false)
                 .Annotation("SqlServer:Identity", "501, 1");

            migrationBuilder.AddColumn<int>(
                name: "CartTypeID",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartTypeID",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "CartTypeID",
                table: "cart");
        }
    }
}
