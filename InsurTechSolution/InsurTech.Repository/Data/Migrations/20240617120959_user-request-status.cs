using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class userrequeststatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8324d19e-4160-402a-a273-cb90454927aa", "AQAAAAIAAYagAAAAEC/72deuZeyFDEpD1zidHaD2yy5FU1DQSPNefls5l5RGmNKH5+Lu24OVbRU2yfDMtw==", "2139de8f-9665-4ed2-9a26-950172903dfc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf7befa0-00b2-4fcc-9967-ee55dc294434", "AQAAAAIAAYagAAAAED2Zjkb2/oe00r/SdpAkR43+JJrD+QqWNIPf544uIoeE73DiAmfAhxNYSMcUXFON7w==", "dc96a64d-7b43-4883-8cae-c2d473e0b7de" });
        }
    }
}
