using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyReadingLog.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameBookStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatuses_StatusId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Books",
                newName: "BookStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_StatusId",
                table: "Books",
                newName: "IX_Books_BookStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books",
                column: "BookStatusId",
                principalTable: "BookStatuses",
                principalColumn: "BookStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "BookStatusId",
                table: "Books",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookStatusId",
                table: "Books",
                newName: "IX_Books_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatuses_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "BookStatuses",
                principalColumn: "BookStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
