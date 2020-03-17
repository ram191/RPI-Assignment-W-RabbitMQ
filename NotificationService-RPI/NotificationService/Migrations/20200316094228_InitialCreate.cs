using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NotificationService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification_logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Notification_id = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    Email_destination = table.Column<string>(nullable: true),
                    Read_at = table.Column<DateTime>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification_logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_logs_Notifications_Notification_id",
                        column: x => x.Notification_id,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_logs_Notification_id",
                table: "Notification_logs",
                column: "Notification_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification_logs");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
