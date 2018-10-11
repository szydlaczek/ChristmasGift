using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmasGiftApp.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGifts",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TargetEmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGifts", x => new { x.EmployeeId, x.TargetEmployeeId, x.Year });
                    table.ForeignKey(
                        name: "FK_EmployeeGifts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeGifts_Employees_TargetEmployeeId",
                        column: x => x.TargetEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "g.gilbert@company.com", "Guy", "Gilbert" },
                    { 2, "k.brown@company.com", "Kevin", "Brown" },
                    { 3, "r.tamburello@company.com", "Roberto", "Tamburello" },
                    { 4, "r.walters@company.com", "Rob", "Walters" },
                    { 5, "d.bradley@company.com", "David", "Bradley" },
                    { 6, "r.ellerbrock@company.com", "Ruth", "Ellerbrock" },
                    { 7, "g.erickson@company.com", "Geil", "Erickson" },
                    { 8, "steven0@adventure-works.com", "Steven", "Selikoff" },
                    { 9, "peter0@adventure-works.com", "Peter", "Krebs" },
                    { 10, "stuart0@adventure-works.com", "Stuart", "Munson" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeGifts_TargetEmployeeId",
                table: "EmployeeGifts",
                column: "TargetEmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeGifts");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
