using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerSystemBackend.Migrations
{
    /// <inheritdoc />
    public partial class cascadeBrisanje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemStates_WorkItemTypes_WorkItemTypeId",
                table: "WorkItemStates");

            migrationBuilder.RenameColumn(
                name: "WorkItemTypeId",
                table: "WorkItemStates",
                newName: "SpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemStates_WorkItemTypeId",
                table: "WorkItemStates",
                newName: "IX_WorkItemStates_SpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemStates_Spaces_SpaceId",
                table: "WorkItemStates",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItemStates_Spaces_SpaceId",
                table: "WorkItemStates");

            migrationBuilder.RenameColumn(
                name: "SpaceId",
                table: "WorkItemStates",
                newName: "WorkItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkItemStates_SpaceId",
                table: "WorkItemStates",
                newName: "IX_WorkItemStates_WorkItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItemStates_WorkItemTypes_WorkItemTypeId",
                table: "WorkItemStates",
                column: "WorkItemTypeId",
                principalTable: "WorkItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
