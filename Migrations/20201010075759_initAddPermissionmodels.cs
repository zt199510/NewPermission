using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPlatform.Migrations
{
    public partial class initAddPermissionmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    URL = table.Column<string>(nullable: true),
                    URLName = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    PermissionModelsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionModels_PermissionModels_PermissionModelsId",
                        column: x => x.PermissionModelsId,
                        principalTable: "PermissionModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionModels_PermissionModelsId",
                table: "PermissionModels",
                column: "PermissionModelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionModels");
        }
    }
}
