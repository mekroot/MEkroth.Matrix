using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MEkroth.Matrix.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusMatrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Statuses = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMatrices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StatusMatrices",
                columns: new[] { "Id", "Name", "Statuses" },
                values: new object[,]
                {
                    { new Guid("45b48626-b2a8-4e28-a28d-a5631db95a95"), "Status Matrix 2", "[2,1,1,1,2,3,3,2,3,2,3,1,3,1,0,0,2,3,2,1,2,0,3,2,1]" },
                    { new Guid("5712ef61-14fc-4032-b58c-ed5a7f02087d"), "Status Matrix 9", "[3,3,3,1,2,2,2,2,1,2,2,2,3,0,3,1,2,3,2,1,1,2,2,1,3]" },
                    { new Guid("688d4e6b-7c46-443c-8a82-2d37383e0747"), "Status Matrix 8", "[1,0,2,2,1,3,3,3,2,3,2,2,2,2,1,3,0,1,1,2,2,3,3,2,0]" },
                    { new Guid("930a4377-7cb4-4c59-805a-9bd4ae77aabc"), "Status Matrix 3", "[2,0,0,0,0,2,2,0,2,0,2,3,2,2,3,1,3,1,1,0,2,2,1,0,0]" },
                    { new Guid("9874769a-9a34-45c7-9eab-6064e2fe1ed6"), "Status Matrix 4", "[0,0,0,2,2,2,3,3,0,3,2,2,0,0,3,1,2,2,2,0,1,3,0,1,2]" },
                    { new Guid("9b520ce2-a36d-437f-a5c9-49f31bf34045"), "Status Matrix 6", "[3,1,1,3,1,3,3,3,1,3,1,3,3,3,2,1,2,2,2,3,3,3,1,2,0]" },
                    { new Guid("bd69b7db-dd45-44ff-85f4-6d4fe1bf75ff"), "Status Matrix 1", "[0,3,1,0,1,3,2,0,3,3,1,0,3,3,1,2,0,1,0,3,3,0,0,2,3]" },
                    { new Guid("c135365b-892d-4783-814a-5bb1ced2f5cf"), "Status Matrix 7", "[0,0,0,1,0,2,0,0,3,2,1,1,0,0,3,3,1,1,1,2,0,3,3,1,2]" },
                    { new Guid("d032d211-0868-4d26-815e-11ff1a6ae6b7"), "Status Matrix 10", "[2,1,2,0,3,2,2,3,2,0,3,1,0,2,1,0,2,1,3,0,1,2,3,3,1]" },
                    { new Guid("e953eee8-b778-4abb-8867-52b47f858e92"), "Status Matrix 5", "[2,2,3,3,2,0,2,0,2,3,3,2,3,1,0,0,3,3,3,2,0,1,0,3,0]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusMatrices");
        }
    }
}
