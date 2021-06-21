using Microsoft.EntityFrameworkCore.Migrations;

namespace SMKMIVLGU.Migrations
{
	public partial class KekMigration : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<double>(
				name: "AverageCoef",
				table: "UserReports",
				type: "float",
				nullable: false,
				defaultValue: 0.0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "AverageCoef",
				table: "UserReports");
		}
	}
}
