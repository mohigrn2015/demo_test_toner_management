using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreIdentityCustom.Migrations
{
    public partial class project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPercentage",
                table: "PaperUseage");

            migrationBuilder.RenameColumn(
                name: "TotalPercentage",
                table: "PaperUseage",
                newName: "TonerPercentage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TonerPercentage",
                table: "PaperUseage",
                newName: "TotalPercentage");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPercentage",
                table: "PaperUseage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
