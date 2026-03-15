using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JK_Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleLocationId = table.Column<int>(type: "int", nullable: false),
                    VehicleStatusId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_VehicleLocation_VehicleLocationId",
                        column: x => x.VehicleLocationId,
                        principalTable: "VehicleLocation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_VehicleStatus_VehicleStatusId",
                        column: x => x.VehicleStatusId,
                        principalTable: "VehicleStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "Make", "Model", "VehicleTypeId" },
                values: new object[,]
                {
                    { 1, "Toyota", "Camry", 1 },
                    { 2, "Honda", "Civic", 1 },
                    { 3, "Ford", "Escape", 2 },
                    { 4, "Toyota", "RAV4", 2 },
                    { 5, "Ford", "F-150", 3 },
                    { 6, "Chevy", "Express", 4 }
                });

            migrationBuilder.InsertData(
                table: "VehicleLocation",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kitchener" },
                    { 2, "Waterloo" },
                    { 3, "Cambridge" },
                    { 4, "Guelph" }
                });

            migrationBuilder.InsertData(
                table: "VehicleStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Available" },
                    { 2, "Reserved" },
                    { 3, "Rented" },
                    { 4, "Maintenance" }
                });

            migrationBuilder.InsertData(
                table: "VehicleType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sedan" },
                    { 2, "SUV" },
                    { 3, "Truck" },
                    { 4, "Van" }
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "Id", "VehicleId", "VehicleLocationId", "VehicleStatusId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 3, 2, 1 },
                    { 4, 4, 2, 3 },
                    { 5, 5, 3, 1 },
                    { 6, 6, 3, 4 },
                    { 7, 1, 4, 1 },
                    { 8, 2, 4, 2 },
                    { 9, 3, 1, 1 },
                    { 10, 4, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VehicleId",
                table: "Inventory",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VehicleLocationId",
                table: "Inventory",
                column: "VehicleLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VehicleStatusId",
                table: "Inventory",
                column: "VehicleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLocation_Name",
                table: "VehicleLocation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleStatus_Name",
                table: "VehicleStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleType_Name",
                table: "VehicleType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropTable(
                name: "VehicleLocation");

            migrationBuilder.DropTable(
                name: "VehicleStatus");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
