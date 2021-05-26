using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCPractice.Migrations
{
    public partial class productmodelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "String",
                table: "Product",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Product",
                newName: "String");
        }
    }
}
