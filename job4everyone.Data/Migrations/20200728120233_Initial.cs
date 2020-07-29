using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace job4everyone.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    JobPositionId = table.Column<int>(nullable: false),
                    EmployerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisements_Employers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "Employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisements_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementsCandidates",
                columns: table => new
                {
                    AdvertisementId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementsCandidates", x => new { x.AdvertisementId, x.CandidateId });
                    table.ForeignKey(
                        name: "FK_AdvertisementsCandidates_Advertisements_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementsCandidates_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "JobPositions",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(3768), "QA", new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4101) },
                    { 2, new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4810), "Developer", new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4820) },
                    { 3, new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4840), "Manager", new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4841) },
                    { 4, new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4842), "DevOps", new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4842) },
                    { 5, new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4843), "PM", new DateTime(2020, 7, 28, 12, 2, 32, 924, DateTimeKind.Utc).AddTicks(4844) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_EmployerId",
                table: "Advertisements",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_JobPositionId",
                table: "Advertisements",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementsCandidates_CandidateId",
                table: "AdvertisementsCandidates",
                column: "CandidateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementsCandidates");

            migrationBuilder.DropTable(
                name: "Advertisements");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "JobPositions");
        }
    }
}
