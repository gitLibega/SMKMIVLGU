using Microsoft.EntityFrameworkCore.Migrations;

namespace SMKMIVLGU.Migrations
{
	public partial class RebuildReports : KekMigration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "idIkProcess",
				table: "UserReports",
				newName: "IkProcessId");

			migrationBuilder.CreateIndex(
				name: "IX_UserReports_IkProcessId",
				table: "UserReports",
				column: "IkProcessId");

			migrationBuilder.AddForeignKey(
				name: "FK_UserReports_IkProcesses_IkProcessId",
				table: "UserReports",
				column: "IkProcessId",
				principalTable: "IkProcesses",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_UserReports_IkProcesses_IkProcessId",
				table: "UserReports");

			migrationBuilder.DropIndex(
				name: "IX_UserReports_IkProcessId",
				table: "UserReports");

			migrationBuilder.RenameColumn(
				name: "IkProcessId",
				table: "UserReports",
				newName: "idIkProcess");
		}
	}
}
