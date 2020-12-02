using Microsoft.EntityFrameworkCore.Migrations;

namespace Mathe_Nachhilfe_Plattform.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vorname = table.Column<string>(nullable: false),
                    nachname = table.Column<string>(nullable: false),
                    adresse = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    mobile = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Dokuments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    DokumentUrl = table.Column<string>(nullable: false),
                    userid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokuments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Dokuments_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokuments_userid",
                table: "Dokuments",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokuments");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
