using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreIdentityCustom.Migrations
{
    public partial class toner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCounter",
                table: "PaperUseage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPercentage",
                table: "PaperUseage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "TonerStocks",
                columns: table => new
                {
                    TonerStockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TonerModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStock = table.Column<int>(type: "int", nullable: false),
                    TotalToner = table.Column<int>(type: "int", nullable: false),
                    CurrentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonerStocks", x => x.TonerStockID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TonerStocks");

            migrationBuilder.DropColumn(
                name: "TotalCounter",
                table: "PaperUseage");

            migrationBuilder.DropColumn(
                name: "TotalPercentage",
                table: "PaperUseage");
        }
    }
}
