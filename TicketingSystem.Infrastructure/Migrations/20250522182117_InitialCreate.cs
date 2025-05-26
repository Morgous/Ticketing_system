using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSeatReservations",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSeatReservations", x => new { x.UserId, x.SeatId });
                    table.ForeignKey(
                        name: "FK_UserSeatReservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSeatReservations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDate", "Location", "Title" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174020"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "My awesome Venue", "Updated Rock Concert" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CartId", "CreatedAt", "EventId", "PaymentId", "PriceId", "SeatId", "Status", "UserId", "Version" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174040"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426614174020"), null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000001"), "Booked", new Guid("123e4567-e89b-12d3-a456-426614174000"), 0 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "OrderId", "PaymentDate", "Status" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174060"), new Guid("123e4567-e89b-12d3-a456-426614174040"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "DateCreated", "OrderId", "Status" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174050"), 150.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("123e4567-e89b-12d3-a456-426614174040"), "Pending" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426614174000"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "1234567890" },
                    { new Guid("123e4567-e89b-12d3-a456-426614174001"), new DateTime(1992, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("bb578f5f-836d-4767-8e98-58d0afbc3ff8"), "Centralna Street 57", "My awesome Venue" });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "Name", "VenueId" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426614174011"), "Updated Section A", new Guid("bb578f5f-836d-4767-8e98-58d0afbc3ff8") },
                    { new Guid("123e4567-e89b-12d3-a456-426614174012"), "Updated Section B", new Guid("bb578f5f-836d-4767-8e98-58d0afbc3ff8") }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "EventId", "IsAvailable", "Row", "SeatNumber", "SectionId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "1", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "2", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "3", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "4", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "5", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "6", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "7", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("123e4567-e89b-12d3-a456-426614174020"), true, "A", "8", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("123e4567-e89b-12d3-a456-426614174030"), new Guid("123e4567-e89b-12d3-a456-426614174020"), false, "B", "3", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 },
                    { new Guid("123e4567-e89b-12d3-a456-426614174031"), new Guid("123e4567-e89b-12d3-a456-426614174020"), false, "B", "4", new Guid("123e4567-e89b-12d3-a456-426614174011"), "Available", 0 }
                });

            migrationBuilder.InsertData(
                table: "UserSeatReservations",
                columns: new[] { "SeatId", "UserId", "ExpiresAt", "ReservedAt" },
                values: new object[,]
                {
                    { new Guid("123e4567-e89b-12d3-a456-426614174030"), new Guid("123e4567-e89b-12d3-a456-426614174000"), new DateTime(2025, 6, 1, 16, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("123e4567-e89b-12d3-a456-426614174031"), new Guid("123e4567-e89b-12d3-a456-426614174001"), new DateTime(2025, 6, 2, 16, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 2, 14, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_EventId",
                table: "Seats",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectionId",
                table: "Seats",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_VenueId",
                table: "Sections",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSeatReservations_SeatId",
                table: "UserSeatReservations",
                column: "SeatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserSeatReservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
