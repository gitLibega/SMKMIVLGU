using Microsoft.EntityFrameworkCore.Migrations;

namespace SMKMIVLGU.Migrations
{
	public partial class kekesi : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "AverageCoef",
				table: "UserReports",
				type: "int",
				nullable: false,
				oldClrType: typeof(double),
				oldType: "float");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<double>(
				name: "AverageCoef",
				table: "UserReports",
				type: "float",
				nullable: false,
				oldClrType: typeof(int),
				oldType: "int");
		}
	}
}
