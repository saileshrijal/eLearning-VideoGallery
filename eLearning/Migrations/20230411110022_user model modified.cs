using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eLearning.Migrations
{
    /// <inheritdoc />
    public partial class usermodelmodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GradeId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GradeId1",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GradeId",
                table: "AspNetUsers",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId",
                table: "AspNetUsers",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GradeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GradeId1",
                table: "AspNetUsers",
                column: "GradeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grades_GradeId1",
                table: "AspNetUsers",
                column: "GradeId1",
                principalTable: "Grades",
                principalColumn: "Id");
        }
    }
}
