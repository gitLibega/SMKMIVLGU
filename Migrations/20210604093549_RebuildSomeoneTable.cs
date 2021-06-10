using Microsoft.EntityFrameworkCore.Migrations;

namespace SMKMIVLGU.Migrations
{
    public partial class RebuildSomeoneTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idIkProcess",
                table: "UserReports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idIkProcess",
                table: "UserReports");
        }
    }
}
