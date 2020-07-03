using Microsoft.EntityFrameworkCore.Migrations;

namespace HackATL_Server.Migrations.SqlServerMigrations
{
    public partial class ChatDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserNames",
                table: "UserChatList_Component");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserChatList_Component",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserChatList_Component",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserChatList_Component");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserChatList_Component");

            migrationBuilder.AddColumn<string>(
                name: "UserNames",
                table: "UserChatList_Component",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
