using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreIdentityCustom.Migrations
{
    public partial class paper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPercentage",
                table: "PaperUseage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStock",
                table: "PaperUseage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalToner",
                table: "PaperUseage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPercentage",
                table: "PaperUseage");

            migrationBuilder.DropColumn(
                name: "CurrentStock",
                table: "PaperUseage");

            migrationBuilder.DropColumn(
                name: "TotalToner",
                table: "PaperUseage");
        }
    }
}
