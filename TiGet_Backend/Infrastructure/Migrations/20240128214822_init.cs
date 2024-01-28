using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vehicleType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeToGo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DestinationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Stations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Stations_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SitPos = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketOwnerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketOwnerLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName", "CreatedDate", "Province" },
                values: new object[,]
                {
                    { new Guid("12d95f4f-9077-4a37-b435-72cb39a57b59"), "Karaj", new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1061), "Karaj" },
                    { new Guid("649c0458-3bd7-4836-a95c-05bb8e3d1dd6"), "Hamedan", new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1062), "Hamedan" },
                    { new Guid("d592c5fb-8a94-4bd1-8404-a07adf270c4f"), "Tehran", new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1060), "Tehran" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "Gender", "LastName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { new Guid("576a1ee3-46e7-4449-a515-1909073f1f0c"), new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(867), "admin@admin.com", "", 0, "", "$2a$11$.64fLerPDfuVgkHnbF3o6uBF1MGQqfxYoPivqq8HkwvevmKIbT5gy", "", 0 });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Capacity", "CreatedDate", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("428cc3e8-8be9-426a-999e-ce71fd29cb8f"), 50, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1032), "Airplane1", 2 },
                    { new Guid("7dd0f551-91a4-4929-896f-f42bd8095a25"), 40, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1015), "Bus2", 0 },
                    { new Guid("806dc0fe-6e6a-4900-bb8d-cb583d27887e"), 70, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1029), "Train1", 1 },
                    { new Guid("b07828fb-82f6-4aa5-9992-a48c9994b932"), 88, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1030), "Train2", 1 },
                    { new Guid("ccc08e7c-2a74-4fef-9efc-0b584ae1dadf"), 20, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1017), "Bus3", 0 },
                    { new Guid("fe0a37a9-ee2e-4195-925b-327a7dafd4dd"), 30, new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1013), "Bus1", 0 }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Address", "CityId", "CreatedDate", "Name", "vehicleType" },
                values: new object[,]
                {
                    { new Guid("086a738e-a1a5-45ca-b3f1-ac1928458500"), "some address", new Guid("d592c5fb-8a94-4bd1-8404-a07adf270c4f"), new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1110), "station 1", 0 },
                    { new Guid("39fa5ea6-c9b3-420a-932e-6cb4b38fc8b5"), "some addres", new Guid("649c0458-3bd7-4836-a95c-05bb8e3d1dd6"), new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1117), "station 3", 0 },
                    { new Guid("a56f9fe4-4657-4115-8501-2e8e110c065a"), "some address", new Guid("12d95f4f-9077-4a37-b435-72cb39a57b59"), new DateTime(2024, 1, 29, 1, 18, 22, 519, DateTimeKind.Local).AddTicks(1112), "station 2", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TicketId",
                table: "Orders",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_CityId",
                table: "Stations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CompanyId",
                table: "Tickets",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DestinationId",
                table: "Tickets",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SourceId",
                table: "Tickets",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VehicleId",
                table: "Tickets",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
