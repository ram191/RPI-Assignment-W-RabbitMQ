using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationService.Migrations
{
    public partial class NotificationEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Notification_logs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Notification_logs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
