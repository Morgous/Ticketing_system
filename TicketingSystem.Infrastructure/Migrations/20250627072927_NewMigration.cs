using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSeatReservations_User_UserId",
                table: "UserSeatReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("6d762474-a6c5-49a1-8461-4a93b2fe4c82"));

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "EventId", "IsAvailable", "Row", "SeatNumber", "SectionId", "Status", "Version" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("b1e8c82c-736f-4a6b-9f10-15d562ee5692"), true, "A", "1", new Guid("03dbdd25-660b-4980-a918-c8d918594d8e"), "Available", 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeatReservations_Users_UserId",
                table: "UserSeatReservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSeatReservations_Users_UserId",
                table: "UserSeatReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "EventId", "IsAvailable", "Row", "SeatNumber", "SectionId", "Status", "Version" },
                values: new object[] { new Guid("6d762474-a6c5-49a1-8461-4a93b2fe4c82"), new Guid("b1e8c82c-736f-4a6b-9f10-15d562ee5692"), true, "A", "1", new Guid("03dbdd25-660b-4980-a918-c8d918594d8e"), "Available", 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSeatReservations_User_UserId",
                table: "UserSeatReservations",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
