using Microsoft.EntityFrameworkCore.Migrations;

namespace restore.Migrations
{
    public partial class StoreInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Storage",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                table: "Storage",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
