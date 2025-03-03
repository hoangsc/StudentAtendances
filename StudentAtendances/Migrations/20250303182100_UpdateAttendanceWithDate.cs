using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentAtendances.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendanceWithDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYears = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LecturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectAttendances", x => new { x.StudentId, x.SubjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_StudentSubjectAttendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentSubjectAttendances_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Lecturers",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "a@example.com", "Nguyen Van A", "hashedpassword1" },
                    { 2, "b@example.com", "Tran Thi B", "hashedpassword2" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "GroupName", "LecturerId", "Major", "SchoolYears" },
                values: new object[,]
                {
                    { 1, "CNTT", 1, "IT", "2023-2024" },
                    { 2, "QTKD", 2, "Business", "2023-2024" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "LecturerId", "SubjectCode", "SubjectName" },
                values: new object[,]
                {
                    { 1, 1, "IT101", "Lập trình C#" },
                    { 2, 1, "IT102", "Cấu trúc dữ liệu" },
                    { 3, 1, "IT103", "SQL Server" },
                    { 4, 2, "BUS201", "Marketing" },
                    { 5, 2, "BUS202", "Quản lý tài chính" },
                    { 6, 2, "BUS203", "Kinh tế vi mô" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "GroupId", "Name", "StudentCode", "StudyCode" },
                values: new object[,]
                {
                    { 1, 1, "Student 1", "SV001", "SC001" },
                    { 2, 1, "Student 2", "SV002", "SC002" },
                    { 3, 1, "Student 3", "SV003", "SC003" },
                    { 4, 1, "Student 4", "SV004", "SC004" },
                    { 5, 1, "Student 5", "SV005", "SC005" },
                    { 6, 2, "Student 6", "SV006", "SC006" },
                    { 7, 2, "Student 7", "SV007", "SC007" },
                    { 8, 2, "Student 8", "SV008", "SC008" },
                    { 9, 2, "Student 9", "SV009", "SC009" },
                    { 10, 2, "Student 10", "SV010", "SC010" }
                });

            migrationBuilder.InsertData(
                table: "StudentSubjectAttendances",
                columns: new[] { "Id", "StudentId", "SubjectId", "Date", "IsPresent" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2, 1, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3, 1, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 19, 1, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 20, 1, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 21, 1, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 37, 1, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 38, 1, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 39, 1, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 55, 1, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 56, 1, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 57, 1, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 73, 1, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 74, 1, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 75, 1, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 91, 1, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 92, 1, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 93, 1, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4, 2, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5, 2, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 6, 2, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 22, 2, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 23, 2, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 24, 2, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 40, 2, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 41, 2, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 42, 2, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 58, 2, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 59, 2, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 60, 2, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 76, 2, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 77, 2, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 78, 2, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 94, 2, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 95, 2, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 96, 2, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 7, 3, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 8, 3, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9, 3, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 25, 3, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 26, 3, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 27, 3, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 43, 3, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 44, 3, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 45, 3, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 61, 3, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 62, 3, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 63, 3, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 79, 3, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 80, 3, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 81, 3, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 97, 3, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 98, 3, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 99, 3, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 10, 4, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 11, 4, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 12, 4, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 28, 4, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 29, 4, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 30, 4, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 46, 4, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 47, 4, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 48, 4, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 64, 4, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 65, 4, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 66, 4, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 82, 4, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 83, 4, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 84, 4, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 100, 4, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 101, 4, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 102, 4, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 13, 5, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 14, 5, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 15, 5, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 31, 5, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 32, 5, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 33, 5, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 49, 5, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 50, 5, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 51, 5, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 67, 5, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 68, 5, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 69, 5, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 85, 5, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 86, 5, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 87, 5, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 103, 5, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 104, 5, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 105, 5, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 16, 6, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 17, 6, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 18, 6, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 34, 6, 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 35, 6, 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 36, 6, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 52, 6, 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 53, 6, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 54, 6, 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 70, 6, 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 71, 6, 4, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 72, 6, 4, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 88, 6, 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 89, 6, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 90, 6, 5, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 106, 6, 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 107, 6, 6, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 108, 6, 6, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LecturerId",
                table: "Groups",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectAttendances_SubjectId",
                table: "StudentSubjectAttendances",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LecturerId",
                table: "Subjects",
                column: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSubjectAttendances");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
