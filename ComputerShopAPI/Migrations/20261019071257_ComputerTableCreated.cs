using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class ComputerTableCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    varchar37 = table.Column<string>(name: "varchar(37)", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    varchar30 = table.Column<string>(name: "varchar(30)", type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    @double = table.Column<float>(name: "double", type: "float", nullable: false),
                    Memory = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computers");
        }
    }
}
