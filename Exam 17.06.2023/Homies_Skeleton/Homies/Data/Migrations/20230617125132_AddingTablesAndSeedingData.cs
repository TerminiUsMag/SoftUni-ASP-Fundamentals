using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homies.Data.Migrations
{
    public partial class AddingTablesAndSeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Name of the type")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                },
                comment: "Types are the categories of the events.");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Primary key")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Name of the event"),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Description of the event"),
                    OrganiserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Id of the user who created the event"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the event was created"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the event starts"),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the event ends"),
                    TypeId = table.Column<int>(type: "int", nullable: false, comment: "Id of the type of the event")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Events are the main part of the app, created by users and joined by other users.");

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    HelperId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Joined user's ID"),
                    EventId = table.Column<int>(type: "int", nullable: false, comment: "Event's ID")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => new { x.EventId, x.HelperId });
                    table.ForeignKey(
                        name: "FK_EventParticipants_AspNetUsers_HelperId",
                        column: x => x.HelperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                },
                comment: "Users who have joined an event.");

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Animals" },
                    { 2, "Fun" },
                    { 3, "Discussion" },
                    { 4, "Work" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_HelperId",
                table: "EventParticipants",
                column: "HelperId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganiserId",
                table: "Events",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TypeId",
                table: "Events",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
