using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library21.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookName = table.Column<string>(nullable: false),
                    BookPublisher = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "BookIssues",
                columns: table => new
                {
                    BookIssueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    MemberAddress = table.Column<string>(nullable: true),
                    BookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookIssues", x => x.BookIssueID);
                    table.ForeignKey(
                        name: "FK_BookIssues_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_BookID",
                table: "BookIssues",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookIssues");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
