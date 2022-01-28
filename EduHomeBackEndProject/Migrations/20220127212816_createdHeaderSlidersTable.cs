using Microsoft.EntityFrameworkCore.Migrations;

namespace EduHomeBackEndProject.Migrations
{
    public partial class createdHeaderSlidersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeaderSliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 70, nullable: true),
                    Title = table.Column<string>(maxLength: 70, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderSliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeaderSliders");
        }
    }
}
