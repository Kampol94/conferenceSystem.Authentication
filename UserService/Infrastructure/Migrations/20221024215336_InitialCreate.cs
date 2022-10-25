using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagementService.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "management");

            migrationBuilder.CreateTable(
                name: "ExhibitionProposals",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProposalUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DecisionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecisionUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DecisionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionRejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExhibitionProposals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExhibitionProposals",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "management");
        }
    }
}
