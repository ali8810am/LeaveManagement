using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Presistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateleaveRequesttbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestngEmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestngEmployeeId",
                table: "LeaveRequests");
        }
    }
}
