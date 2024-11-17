using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lab_6.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingStatuses",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(3)", nullable: false),
                    Description = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingStatuses", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Details = table.Column<string>(type: "varchar(2000)", nullable: false),
                    Gender = table.Column<string>(type: "char(1)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "varchar(255)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "varchar(255)", nullable: false),
                    AddressLine3 = table.Column<string>(type: "varchar(255)", nullable: false),
                    Town = table.Column<string>(type: "varchar(50)", nullable: false),
                    County = table.Column<string>(type: "varchar(30)", nullable: false),
                    Country = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(10)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Details = table.Column<string>(type: "varchar(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(10)", nullable: false),
                    DailyHireRate = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "VehicleCategories",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(5)", nullable: false),
                    Description = table.Column<string>(type: "char(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCategories", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    RegNumber = table.Column<string>(type: "char(10)", nullable: false),
                    CurrentMileage = table.Column<int>(type: "integer", nullable: false),
                    DailyHireRate = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    DateMotDue = table.Column<DateTime>(type: "date", nullable: false),
                    ManufacturerCode = table.Column<string>(type: "char(10)", nullable: false),
                    ModelCode = table.Column<string>(type: "char(10)", nullable: false),
                    VehicleCategoryCode = table.Column<string>(type: "char(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.RegNumber);
                    table.ForeignKey(
                        name: "FK_Vehicles_Manufacturers_ManufacturerCode",
                        column: x => x.ManufacturerCode,
                        principalTable: "Manufacturers",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelCode",
                        column: x => x.ModelCode,
                        principalTable: "Models",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleCategories_VehicleCategoryCode",
                        column: x => x.VehicleCategoryCode,
                        principalTable: "VehicleCategories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateFrom = table.Column<DateTime>(type: "date", nullable: false),
                    DateTo = table.Column<DateTime>(type: "date", nullable: false),
                    IsConfirmationLetterSent = table.Column<string>(type: "char(1)", nullable: false),
                    IsPaymentReceived = table.Column<string>(type: "char(1)", nullable: false),
                    BookingStatusCode = table.Column<string>(type: "char(3)", nullable: false),
                    VehicleRegNumber = table.Column<string>(type: "char(10)", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_BookingStatuses_BookingStatusCode",
                        column: x => x.BookingStatusCode,
                        principalTable: "BookingStatuses",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Vehicles_VehicleRegNumber",
                        column: x => x.VehicleRegNumber,
                        principalTable: "Vehicles",
                        principalColumn: "RegNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookingStatuses",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { "CAN", "Cancelled" },
                    { "COM", "Completed" },
                    { "CON", "Confirmed" },
                    { "EXP", "Expired" },
                    { "HOL", "On hold" },
                    { "NEW", "New" },
                    { "NOS", "No-show" },
                    { "PAI", "Paid" },
                    { "PEN", "Pending" },
                    { "WAI", "Waiting" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressLine1", "AddressLine2", "AddressLine3", "Country", "County", "Details", "Email", "Gender", "Name", "Phone", "Town" },
                values: new object[,]
                {
                    { 1, "123 Elm St", "Apt 4B", "", "USA", "Illinois", "Regular customer", "john.doe@example.com", "M", "John Doe", "123-456-7890", "Springfield" },
                    { 2, "456 Oak St", "", "", "USA", "Connecticut", "VIP customer", "jane.smith@example.com", "F", "Jane Smith", "987-654-3210", "Greenwich" },
                    { 3, "789 Pine St", "Suite 100", "", "USA", "Illinois", "Occasional customer", "michael.johnson@example.com", "M", "Michael Johnson", "555-123-4567", "Chicago" },
                    { 4, "101 Maple Ave", "", "", "USA", "Massachusetts", "Loyal customer", "emily.brown@example.com", "F", "Emily Brown", "222-333-4444", "Boston" },
                    { 5, "202 Birch Rd", "Unit 5", "", "USA", "California", "First-time customer", "william.davis@example.com", "M", "William Davis", "444-555-6666", "Los Angeles" },
                    { 6, "303 Cedar Dr", "Floor 3", "", "USA", "Florida", "Frequent traveler", "sophia.miller@example.com", "F", "Sophia Miller", "555-987-6543", "Miami" },
                    { 7, "404 Pinecrest Rd", "", "", "USA", "Texas", "Preferred customer", "daniel.wilson@example.com", "M", "Daniel Wilson", "888-777-6666", "Dallas" },
                    { 8, "505 Birch St", "Apartment 2A", "", "USA", "Texas", "Customer with VIP status", "olivia.garcia@example.com", "F", "Olivia Garcia", "666-555-4444", "Houston" },
                    { 9, "606 Willow Ln", "", "", "USA", "Washington", "New customer", "lucas.martinez@example.com", "M", "Lucas Martinez", "111-222-3333", "Seattle" },
                    { 10, "707 Redwood Ave", "", "", "USA", "California", "Occasional customer", "ava.rodriguez@example.com", "F", "Ava Rodriguez", "777-888-9999", "San Francisco" }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Code", "Details", "Name" },
                values: new object[,]
                {
                    { "BMW", "German multinational company which produces luxury vehicles and motorcycles", "Bayerische Motoren Werke" },
                    { "FORD", "American multinational automaker", "Ford Motor Company" },
                    { "HONDA", "Japanese multinational conglomerate known for manufacturing automobiles, motorcycles, and power equipment", "Honda Motor Co." },
                    { "TOYO", "Japanese multinational automotive manufacturer", "Toyota Motor Corporation" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Code", "DailyHireRate", "Name" },
                values: new object[,]
                {
                    { "CIVIC", 65.00m, "Honda Civic" },
                    { "COROLLA", 60.00m, "Toyota Corolla" },
                    { "FIESTA", 50.00m, "Ford Fiesta" },
                    { "M3", 120.00m, "BMW M3" }
                });

            migrationBuilder.InsertData(
                table: "VehicleCategories",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { "ECON", "Economy" },
                    { "LUX", "Luxury" },
                    { "SUV", "SUV" },
                    { "VAN", "Van" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "RegNumber", "CurrentMileage", "DailyHireRate", "DateMotDue", "ManufacturerCode", "ModelCode", "VehicleCategoryCode" },
                values: new object[,]
                {
                    { "ABC123", 15000, 50.00m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FORD", "FIESTA", "ECON" },
                    { "DEF234", 18000, 80.00m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FORD", "FIESTA", "SUV" },
                    { "GHI567", 22000, 110.00m, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TOYO", "COROLLA", "VAN" },
                    { "LMN456", 30000, 65.00m, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HONDA", "CIVIC", "LUX" },
                    { "PQR789", 12000, 120.00m, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BMW", "M3", "LUX" },
                    { "XYZ987", 25000, 60.00m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TOYO", "COROLLA", "ECON" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingStatusCode", "CustomerId", "DateFrom", "DateTo", "IsConfirmationLetterSent", "IsPaymentReceived", "VehicleRegNumber" },
                values: new object[,]
                {
                    { 1, "NEW", 1, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "ABC123" },
                    { 2, "CON", 2, new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "XYZ987" },
                    { 3, "CAN", 3, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "LMN456" },
                    { 4, "PEN", 4, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "PQR789" },
                    { 5, "WAI", 5, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "DEF234" },
                    { 6, "CON", 6, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "GHI567" },
                    { 7, "COM", 7, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "ABC123" },
                    { 8, "EXP", 8, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "N", "XYZ987" },
                    { 9, "NEW", 9, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "LMN456" },
                    { 10, "CAN", 10, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "PQR789" },
                    { 11, "PEN", 1, new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "DEF234" },
                    { 12, "WAI", 2, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "GHI567" },
                    { 13, "CON", 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "ABC123" },
                    { 14, "COM", 4, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "XYZ987" },
                    { 15, "EXP", 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "LMN456" },
                    { 16, "WAI", 6, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "PQR789" },
                    { 17, "PEN", 7, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "DEF234" },
                    { 18, "NEW", 8, new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "Y", "GHI567" },
                    { 19, "CAN", 9, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "N", "Y", "ABC123" },
                    { 20, "CON", 10, new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Y", "N", "XYZ987" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatusCode",
                table: "Bookings",
                column: "BookingStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VehicleRegNumber",
                table: "Bookings",
                column: "VehicleRegNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ManufacturerCode",
                table: "Vehicles",
                column: "ManufacturerCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelCode",
                table: "Vehicles",
                column: "ModelCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleCategoryCode",
                table: "Vehicles",
                column: "VehicleCategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "BookingStatuses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "VehicleCategories");
        }
    }
}
