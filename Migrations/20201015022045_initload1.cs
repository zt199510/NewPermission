using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPlatform.Migrations
{
    public partial class initload1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionModelsId",
                table: "PermissionModels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionModels_PermissionModelsId",
                table: "PermissionModels",
                column: "PermissionModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionModels_PermissionModels_PermissionModelsId",
                table: "PermissionModels",
                column: "PermissionModelsId",
                principalTable: "PermissionModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionModels_PermissionModels_PermissionModelsId",
                table: "PermissionModels");

            migrationBuilder.DropIndex(
                name: "IX_PermissionModels_PermissionModelsId",
                table: "PermissionModels");

            migrationBuilder.DropColumn(
                name: "PermissionModelsId",
                table: "PermissionModels");
        }
    }
}
