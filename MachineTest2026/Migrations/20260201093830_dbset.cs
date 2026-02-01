using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MachineTest2026.Migrations
{
    public partial class dbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryTBL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTBL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTBL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTBL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpTBL",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpTBL_CountryTBL_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryTBL",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmpProfile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpProfileFiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpProfile_EmpTBL_EmpId",
                        column: x => x.EmpId,
                        principalTable: "EmpTBL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpProfile_EmpId",
                table: "EmpProfile",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpTBL_CountryId",
                table: "EmpTBL",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpProfile");

            migrationBuilder.DropTable(
                name: "UserTBL");

            migrationBuilder.DropTable(
                name: "EmpTBL");

            migrationBuilder.DropTable(
                name: "CountryTBL");
        }
    }
}
