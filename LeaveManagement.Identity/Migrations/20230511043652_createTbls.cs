using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Identity.Migrations
{
    /// <inheritdoc />
    public partial class createTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "113d3271-df9b-4910-8616-329555d5ba2b", "AQAAAAIAAYagAAAAEOGXReyxRuufjlLb6hJEWR8PHFCSjG1z+tHCrbc3/t9BlQ7Y44YVLhTN0u+LRpz/JA==", "dd74294f-9e5c-4040-ad9a-2b97e966bcf3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2abfd9a-1d0d-40e9-9c75-ba097aab8f3a", "AQAAAAIAAYagAAAAEN1rJUNtw77KSYs2jKIc1jNylSGZFlfpTDpOpNiZz5WA2rhJbiS/H58J2rkMMa/L4w==", "63a0d1ee-562d-4ae9-abc5-b90b10d4a616" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c6d1043-aea4-4480-a1e9-40fbb601d574", "AQAAAAIAAYagAAAAEAaoSpAgYKl03ELyBUkVUhgUvHfQux/riBuNKJRUWjUO7+9M0hmawft2J1dgxb8WQQ==", "1020d3a9-631d-4952-8303-3a8712b4ad2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b11ac524-a597-4696-949d-9a1499b1a7fe", "AQAAAAIAAYagAAAAECXHawVKJPsKjnmM3rAAxfKpN98+7MEJo/euabP3wFaVPuz7G0F/offTbIdgzX65fQ==", "ffde4301-fba5-4938-837a-a9309daa8ea3" });
        }
    }
}
