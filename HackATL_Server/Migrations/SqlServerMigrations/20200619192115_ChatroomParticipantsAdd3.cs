using Microsoft.EntityFrameworkCore.Migrations;

namespace HackATL_Server.Migrations.SqlServerMigrations
{
    public partial class ChatroomParticipantsAdd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatRoom_participantsRId",
                table: "UserChatList_Component",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "chatRoom_Participants",
                columns: table => new
                {
                    RId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatRoom_Participants", x => x.RId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatList_Component_ChatRoom_participantsRId",
                table: "UserChatList_Component",
                column: "ChatRoom_participantsRId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatList_Component_chatRoom_Participants_ChatRoom_participantsRId",
                table: "UserChatList_Component",
                column: "ChatRoom_participantsRId",
                principalTable: "chatRoom_Participants",
                principalColumn: "RId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChatList_Component_chatRoom_Participants_ChatRoom_participantsRId",
                table: "UserChatList_Component");

            migrationBuilder.DropTable(
                name: "chatRoom_Participants");

            migrationBuilder.DropIndex(
                name: "IX_UserChatList_Component_ChatRoom_participantsRId",
                table: "UserChatList_Component");

            migrationBuilder.DropColumn(
                name: "ChatRoom_participantsRId",
                table: "UserChatList_Component");
        }
    }
}
