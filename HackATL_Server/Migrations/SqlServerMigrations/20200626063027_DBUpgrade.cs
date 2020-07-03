using Microsoft.EntityFrameworkCore.Migrations;

namespace HackATL_Server.Migrations.SqlServerMigrations
{
    public partial class DBUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalInfo",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookUsername",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUsername",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedUsername",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUsername",
                table: "User_Public",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "User_Public",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users_Personal",
                columns: table => new
                {
                    Uid = table.Column<string>(nullable: false),
                    University = table.Column<string>(nullable: true),
                    LinkedUsername = table.Column<string>(nullable: true),
                    FacebookUsername = table.Column<string>(nullable: true),
                    InstagramUsername = table.Column<string>(nullable: true),
                    TwitterUsername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Personal", x => x.Uid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users_Personal");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FacebookUsername",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InstagramUsername",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkedUsername",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwitterUsername",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdditionalInfo",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "FacebookUsername",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "InstagramUsername",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "LinkedUsername",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "TwitterUsername",
                table: "User_Public");

            migrationBuilder.DropColumn(
                name: "University",
                table: "User_Public");
        }
    }
}
