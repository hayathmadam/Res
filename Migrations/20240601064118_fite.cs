using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagment.Migrations
{
    /// <inheritdoc />
    public partial class fite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qunantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    T_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullItems", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullItems");
        }
    }
}
