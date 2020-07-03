using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackATL_Server.Migrations.SqlServerMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaItems",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    Datetime = table.Column<DateTime>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Cateogory = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    Speaker_one = table.Column<string>(nullable: true),
                    Speaker_two = table.Column<string>(nullable: true),
                    ImageSource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User_GroupChatList",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_GroupChatList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChatLog",
                columns: table => new
                {
                    UId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatLog", x => x.UId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChatList_Group",
                columns: table => new
                {
                    RId = table.Column<string>(nullable: false),
                    UserChat_LogListId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatList_Group", x => x.RId);
                    table.ForeignKey(
                        name: "FK_UserChatList_Group_User_GroupChatList_UserChat_LogListId",
                        column: x => x.UserChat_LogListId,
                        principalTable: "User_GroupChatList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserChatList_LogHistory",
                columns: table => new
                {
                    RId = table.Column<string>(nullable: false),
                    UserChatUId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatList_LogHistory", x => x.RId);
                    table.ForeignKey(
                        name: "FK_UserChatList_LogHistory_UserChatLog_UserChatUId",
                        column: x => x.UserChatUId,
                        principalTable: "UserChatLog",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserChatList_Component",
                columns: table => new
                {
                    Uid = table.Column<string>(nullable: false),
                    UserNames = table.Column<string>(nullable: true),
                    UserChatList_GroupRId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatList_Component", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_UserChatList_Component_UserChatList_Group_UserChatList_GroupRId",
                        column: x => x.UserChatList_GroupRId,
                        principalTable: "UserChatList_Group",
                        principalColumn: "RId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_chatlog",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserChatList_LogHistoryRId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_chatlog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_chatlog_UserChatList_LogHistory_UserChatList_LogHistoryRId",
                        column: x => x.UserChatList_LogHistoryRId,
                        principalTable: "UserChatList_LogHistory",
                        principalColumn: "RId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_chatlog_UserChatList_LogHistoryRId",
                table: "User_chatlog",
                column: "UserChatList_LogHistoryRId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatList_Component_UserChatList_GroupRId",
                table: "UserChatList_Component",
                column: "UserChatList_GroupRId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatList_Group_UserChat_LogListId",
                table: "UserChatList_Group",
                column: "UserChat_LogListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatList_LogHistory_UserChatUId",
                table: "UserChatList_LogHistory",
                column: "UserChatUId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaItems");

            migrationBuilder.DropTable(
                name: "User_chatlog");

            migrationBuilder.DropTable(
                name: "UserChatList_Component");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserChatList_LogHistory");

            migrationBuilder.DropTable(
                name: "UserChatList_Group");

            migrationBuilder.DropTable(
                name: "UserChatLog");

            migrationBuilder.DropTable(
                name: "User_GroupChatList");
        }
    }
}
