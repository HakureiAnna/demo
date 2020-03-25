using Microsoft.EntityFrameworkCore.Migrations;

namespace inventory.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 1, "Apples", 100 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 2, "Bananas", 150 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Quantity" },
                values: new object[] { 3, "Cakes", 200 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
