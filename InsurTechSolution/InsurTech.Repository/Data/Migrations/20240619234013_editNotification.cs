using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class editNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "12eba7e0-33ce-4101-9c75-983a9221af15", "AQAAAAIAAYagAAAAEDO92CCHD+yaewcECcfixeZ0BE9UvgAXqCxKtlNPURfp4/3jskKCbcKpm1KBxNmWaQ==", "91c74ba0-27da-407f-a39f-3f0a4df48132" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8324d19e-4160-402a-a273-cb90454927aa", "AQAAAAIAAYagAAAAEC/72deuZeyFDEpD1zidHaD2yy5FU1DQSPNefls5l5RGmNKH5+Lu24OVbRU2yfDMtw==", "2139de8f-9665-4ed2-9a26-950172903dfc" });
        }
    }
}
