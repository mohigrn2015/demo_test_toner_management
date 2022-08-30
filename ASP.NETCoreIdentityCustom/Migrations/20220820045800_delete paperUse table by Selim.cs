using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCoreIdentityCustom.Migrations
{
    public partial class deletepaperUsetablebySelim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaperUseg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaperUseg",
                columns: table => new
                {
                    PaperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    CurrentCounter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousCounter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCounter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPerchent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperUseg", x => x.PaperID);
                    table.ForeignKey(
                        name: "FK_PaperUseg_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaperUseg_MachineId",
                table: "PaperUseg",
                column: "MachineId");
        }
    }
}
