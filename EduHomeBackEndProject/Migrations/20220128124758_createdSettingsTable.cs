using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEndProject.Migrations
{
    public partial class createdSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(maxLength: 50, nullable: false),
                    FooterLogo = table.Column<string>(maxLength: 50, nullable: false),
                    SearchIcon = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber1 = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber2 = table.Column<string>(maxLength: 50, nullable: true),
                    Mail = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    AboutContentTitle = table.Column<string>(maxLength: 50, nullable: true),
                    AboutContentDesc = table.Column<string>(maxLength: 500, nullable: true),
                    AboutContentImage = table.Column<string>(maxLength: 70, nullable: true),
                    AboutContentUrl = table.Column<string>(maxLength: 150, nullable: true),
                    WebAddress = table.Column<string>(maxLength: 50, nullable: true),
                    FooterDescription = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
