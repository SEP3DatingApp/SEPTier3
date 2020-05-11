using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class arturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_userId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "userType",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "sexPref",
                table: "Users",
                newName: "SexPref");

            migrationBuilder.RenameColumn(
                name: "picRef",
                table: "Users",
                newName: "PicRef");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Users",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Users",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Matches",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "matchId",
                table: "Matches",
                newName: "MatchId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_userId",
                table: "Matches",
                newName: "IX_Matches_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_UserId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "SexPref",
                table: "Users",
                newName: "sexPref");

            migrationBuilder.RenameColumn(
                name: "PicRef",
                table: "Users",
                newName: "picRef");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Users",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Matches",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "Matches",
                newName: "matchId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_UserId",
                table: "Matches",
                newName: "IX_Matches_userId");

            migrationBuilder.AddColumn<string>(
                name: "userType",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_userId",
                table: "Matches",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
