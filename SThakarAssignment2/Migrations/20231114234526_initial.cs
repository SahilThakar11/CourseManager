using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SThakarAssignment2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instructor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Instructor", "Name", "RoomNumber", "StartDate" },
                values: new object[,]
                {
                    { 1, "Mark Rajkumar", "Microsoft Web Technologies", "3G17", new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Mark Rajkumar", "Game Programming With Data Structures", "4G11", new DateTime(2022, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "CourseId", "Email", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 1, "sahilgamer3113@gmail.com", "Sahil Thakar", 0 },
                    { 2, 2, "test@gmail.com", "Het Mehta", 0 },
                    { 3, 1, "JP@yahoo.com", "Jeeshant Patel", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
