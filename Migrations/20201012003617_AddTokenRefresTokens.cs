using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPlatform.Migrations
{
    public partial class AddTokenRefresTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRefreshToken",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserInfoUserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshToken_UserInfos_UserInfoUserName",
                        column: x => x.UserInfoUserName,
                        principalTable: "UserInfos",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshToken_UserInfoUserName",
                table: "UserRefreshToken",
                column: "UserInfoUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRefreshToken");
        }
    }
}
