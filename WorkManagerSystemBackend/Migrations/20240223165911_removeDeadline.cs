using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkManagerSystemBackend.Migrations
{
    /// <inheritdoc />
    public partial class removeDeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "WorkItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Deadline",
                table: "WorkItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
