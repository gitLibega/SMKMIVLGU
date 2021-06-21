using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SMKMIVLGU.Migrations
{
	public partial class AddingUserReportsTable : KekMigration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "UserReports",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
					RowHtmlCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Season = table.Column<string>(type: "nvarchar(max)", nullable: true),
					LoadTime = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserReports", x => x.Id);
					table.ForeignKey(
						name: "FK_UserReports_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_UserReports_UserId",
				table: "UserReports",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserReports");
		}
	}
}
