using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VMS1.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigationPropertytoFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EventId",
                table: "Feedbacks",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Events_EventId",
                table: "Feedbacks",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Events_EventId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_EventId",
                table: "Feedbacks");
        }
    }
}
