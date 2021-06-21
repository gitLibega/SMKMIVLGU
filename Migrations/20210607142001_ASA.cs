using Microsoft.EntityFrameworkCore.Migrations;

namespace SMKMIVLGU.Migrations
{
	public partial class ASA : KekMigration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "IkProcessViewModelid",
				table: "configurationPatterns",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "PatternIkProcessRowViewModelId",
				table: "configurationPatterns",
				type: "int",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_configurationPatterns_IkProcessViewModelid",
				table: "configurationPatterns",
				column: "IkProcessViewModelid");

			migrationBuilder.CreateIndex(
				name: "IX_configurationPatterns_PatternIkProcessRowViewModelId",
				table: "configurationPatterns",
				column: "PatternIkProcessRowViewModelId");

			migrationBuilder.AddForeignKey(
				name: "FK_configurationPatterns_IkProcesses_IkProcessViewModelid",
				table: "configurationPatterns",
				column: "IkProcessViewModelid",
				principalTable: "IkProcesses",
				principalColumn: "id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_configurationPatterns_patternIkProcessRows_PatternIkProcessRowViewModelId",
				table: "configurationPatterns",
				column: "PatternIkProcessRowViewModelId",
				principalTable: "patternIkProcessRows",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_configurationPatterns_IkProcesses_IkProcessViewModelid",
				table: "configurationPatterns");

			migrationBuilder.DropForeignKey(
				name: "FK_configurationPatterns_patternIkProcessRows_PatternIkProcessRowViewModelId",
				table: "configurationPatterns");

			migrationBuilder.DropIndex(
				name: "IX_configurationPatterns_IkProcessViewModelid",
				table: "configurationPatterns");

			migrationBuilder.DropIndex(
				name: "IX_configurationPatterns_PatternIkProcessRowViewModelId",
				table: "configurationPatterns");

			migrationBuilder.DropColumn(
				name: "IkProcessViewModelid",
				table: "configurationPatterns");

			migrationBuilder.DropColumn(
				name: "PatternIkProcessRowViewModelId",
				table: "configurationPatterns");
		}
	}
}
